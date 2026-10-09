namespace HotelSystem
{
    public class Guest
    {
        private static readonly HashSet<int> usedGuestIds = new();
        private readonly List<Reservation> reservations = new();

        public int GuestId { get; }
        public string FullName { get; }
        public string PhoneNumber { get; }
        public IReadOnlyList<Reservation> Reservations => reservations.AsReadOnly();

        public Guest(int guestId, string fullName, string phoneNumber)
        {
            if (guestId <= 0)
                throw new ArgumentException("Guest ID must be positive");

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name cannot be empty");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number cannot be empty");

            if (usedGuestIds.Contains(guestId))
                throw new ArgumentException("Guest ID already exists");

            GuestId = guestId;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            usedGuestIds.Add(guestId);
        }

        public Reservation MakeReservation(
            int reservationId, Room room, DateTime checkIn, DateTime checkOut)
        {
            Reservation reservation = new Reservation(
                reservationId, this, room, checkIn, checkOut);

            reservations.Add(reservation);
            return reservation;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"Guest ID: {GuestId}");
            Console.WriteLine($"Full Name: {FullName}");
            Console.WriteLine($"Phone: {PhoneNumber}");
            Console.WriteLine($"Reservations: {reservations.Count}");
        }
    }
}
