using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Models;

public class Room
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(10)]
    public string BuildingCode { get; set; } = string.Empty;

    public int Floor { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
    public int Capacity { get; set; }

    public bool HasProjector { get; set; }

    public bool IsActive { get; set; } = true;
}