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

    public static List<Reservation> Reservations { get; } = [
        new Reservation { Id = 1, RoomId = 1, OrganizerName = "Pan A", Topic = "APBD", Date = new DateOnly(2026, 5, 15), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(10, 0), Status = "confirmed" },
        new Reservation { Id = 2, RoomId = 1, OrganizerName = "Pani G", Topic = "TPO", Date = new DateOnly(2026, 5, 15), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(13, 0), Status = "confirmed" },
        new Reservation { Id = 3, RoomId = 2, OrganizerName = "Pan R", Topic = "PRI", Date = new DateOnly(2026, 5, 16), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(11, 30), Status = "planned" },
        new Reservation { Id = 4, RoomId = 3, OrganizerName = "Pani B", Topic = "ZPR", Date = new DateOnly(2026, 5, 17), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(15, 0), Status = "planned" },
        new Reservation { Id = 5, RoomId = 4, OrganizerName = "Pan M", Topic = "MAS", Date = new DateOnly(2026, 5, 18), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(12, 0), Status = "cancelled" },
        new Reservation { Id = 6, RoomId = 2, OrganizerName = "Pani W", Topic = "BYT", Date = new DateOnly(2026, 5, 19), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(16, 0), Status = "confirmed" },
    ];

    private static int _nextRoomId = 9;
    private static int _nextReservationId = 7;

    public static int NextRoomId() => _nextRoomId++;
    public static int NextReservationId() => _nextReservationId++;
}