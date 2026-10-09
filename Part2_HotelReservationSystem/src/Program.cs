using HotelSystem;

class Program
{
    static void Main()
    {
        Guest guest = new Guest(1, "Ahmed", "01012345678");
        Room room = new Room(101, RoomType.Single, 1000m);

        Reservation first = guest.MakeReservation(
            1, room, new DateTime(2026, 10, 10), new DateTime(2026, 10, 13));

        first.PrintDetails();
        first.Confirm();
        first.CheckIn();
        first.CheckOut();
        first.PrintDetails();

        room.ChangePrice(1200m);
        Reservation second = guest.MakeReservation(
            2, room, new DateTime(2026, 10, 15), new DateTime(2026, 10, 17));

        second.Cancel();
        second.PrintDetails();

        room.StartMaintenance();
        try
        {
            guest.MakeReservation(
                3, room, new DateTime(2026, 10, 20), new DateTime(2026, 10, 22));
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Rejected booking: {ex.Message}");
        }
        room.EndMaintenance();

        Reservation third = guest.MakeReservation(
            3, room, new DateTime(2026, 10, 20), new DateTime(2026, 10, 22));
        third.Confirm();
        third.PrintDetails();

        Guest otherGuest = new Guest(2, "Mona", "01098765432");
        try
        {
            otherGuest.MakeReservation(
                4, room, new DateTime(2026, 10, 21), new DateTime(2026, 10, 23));
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Rejected booking: {ex.Message}");
        }

        guest.PrintDetails();
    }
}
