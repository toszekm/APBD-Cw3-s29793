using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Models;

public class Reservation
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    [Required, MaxLength(100)]
    public string OrganizerName { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Topic { get; set; } = string.Empty;

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    [Required]
    public string Status { get; set; } = "planned";
}