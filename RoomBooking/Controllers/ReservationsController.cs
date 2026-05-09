using Microsoft.AspNetCore.Mvc;
using RoomBooking.Data;
using RoomBooking.Models;

namespace RoomBooking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        var result = AppData.Reservations.AsEnumerable();

        if (date.HasValue)
            result = result.Where(r => r.Date == date.Value);
        if (!string.IsNullOrEmpty(status))
            result = result.Where(r => r.Status == status);
        if (roomId.HasValue)
            result = result.Where(r => r.RoomId == roomId.Value);

        return Ok(result.ToList());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);
        return reservation is null ? NotFound($"Reservation {id} not found.") : Ok(reservation);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Reservation reservation)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        if (room is null)
            return NotFound($"Room {reservation.RoomId} not found.");
        if (!room.IsActive)
            return BadRequest($"Room {reservation.RoomId} is not active.");
        if (reservation.EndTime <= reservation.StartTime)
            return BadRequest("EndTime must be later than StartTime.");

        var hasConflict = AppData.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date &&
            reservation.StartTime < r.EndTime &&
            reservation.EndTime > r.StartTime);

        if (hasConflict)
            return Conflict("A reservation for this room already exists in the given time slot.");

        reservation.Id = AppData.NextReservationId();
        AppData.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] Reservation reservation)
    {
        var existing = AppData.Reservations.FirstOrDefault(r => r.Id == id);
        if (existing is null)
            return NotFound($"Reservation {id} not found.");

        if (reservation.EndTime <= reservation.StartTime)
            return BadRequest("EndTime must be later than StartTime.");

        var hasConflict = AppData.Reservations.Any(r =>
            r.Id != id &&
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date &&
            reservation.StartTime < r.EndTime &&
            reservation.EndTime > r.StartTime);

        if (hasConflict)
            return Conflict("A reservation for this room already exists in the given time slot.");

        existing.RoomId = reservation.RoomId;
        existing.OrganizerName = reservation.OrganizerName;
        existing.Topic = reservation.Topic;
        existing.Date = reservation.Date;
        existing.StartTime = reservation.StartTime;
        existing.EndTime = reservation.EndTime;
        existing.Status = reservation.Status;

        return Ok(existing);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation is null)
            return NotFound($"Reservation {id} not found.");

        AppData.Reservations.Remove(reservation);
        return NoContent();
    }
}