using Microsoft.AspNetCore.Mvc;
using RoomBooking.Data;
using RoomBooking.Models;

namespace RoomBooking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        var result = AppData.Rooms.AsEnumerable();

        if (minCapacity.HasValue)
            result = result.Where(r => r.Capacity >= minCapacity.Value);
        if (hasProjector.HasValue)
            result = result.Where(r => r.HasProjector == hasProjector.Value);
        if (activeOnly == true)
            result = result.Where(r => r.IsActive);

        return Ok(result.ToList());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);
        return room is null ? NotFound($"Room {id} not found.") : Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public IActionResult GetByBuilding([FromRoute] string buildingCode)
    {
        var rooms = AppData.Rooms.Where(r => r.BuildingCode == buildingCode).ToList();
        return Ok(rooms);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Room room)
    {
        room.Id = AppData.NextRoomId();
        AppData.Rooms.Add(room);
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] Room room)
    {
        var existing = AppData.Rooms.FirstOrDefault(r => r.Id == id);
        if (existing is null)
            return NotFound($"Room {id} not found.");

        existing.Name = room.Name;
        existing.BuildingCode = room.BuildingCode;
        existing.Floor = room.Floor;
        existing.Capacity = room.Capacity;
        existing.HasProjector = room.HasProjector;
        existing.IsActive = room.IsActive;

        return Ok(existing);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);
        if (room is null)
            return NotFound($"Room {id} not found.");

        var hasFutureReservations = AppData.Reservations.Any(r =>
            r.RoomId == id && r.Date >= DateOnly.FromDateTime(DateTime.Today));

        if (hasFutureReservations)
            return Conflict($"Room {id} has future reservations and cannot be deleted.");

        AppData.Rooms.Remove(room);
        return NoContent();
    }
}