using RoomBooking.Models;

namespace RoomBooking.Data;

public static class AppData
{
    public static List<Room> Rooms { get; } =
    [
        new Room { Id = 1, Name = "B101", BuildingCode = "B", Floor = 1, Capacity = 20, HasProjector = true, IsActive = true },
        new Room { Id = 2, Name = "B102", BuildingCode = "B", Floor = 1, Capacity = 20, HasProjector = false, IsActive = true },
        new Room { Id = 3, Name = "A364", BuildingCode = "A", Floor = 3, Capacity = 24, HasProjector = true, IsActive = true },
        new Room { Id = 4, Name = "A221", BuildingCode = "A", Floor = 2, Capacity = 40, HasProjector = false, IsActive = true },
        new Room { Id = 5, Name = "A352", BuildingCode = "A", Floor = 3, Capacity = 35, HasProjector = true, IsActive = true },
        new Room { Id = 6, Name = "Wykładowa A", BuildingCode = "A", Floor = 1, Capacity = 150, HasProjector = true, IsActive = true },
        new Room { Id = 7, Name = "Wykładowa B", BuildingCode = "B", Floor = 1, Capacity = 120, HasProjector = true, IsActive = true },
        new Room { Id = 8, Name = "Magazyn", BuildingCode = "B", Floor = 0, Capacity = 5, HasProjector = false, IsActive = false },
    ];

    public static List<Reservation> Reservations { get; } = [];

    private static int _nextRoomId = 9;
    private static int _nextReservationId = 1;

    public static int NextRoomId() => _nextRoomId++;
    public static int NextReservationId() => _nextReservationId++;
}