namespace HotelSystem
{
    public enum ReservationStatus
    {
        Pending,
        Confirmed,
        CheckedIn,
        CheckedOut,
        Cancelled
    }

    public class Reservation
    {
        private static readonly HashSet<int> usedReservationIds = new();

        public int ReservationId { get; }
        public Guest Guest { get; }
        public Room Room { get; }
        public DateTime CheckInDate { get; }
        public DateTime CheckOutDate { get; }
        public ReservationStatus Status { get; private set; }
        public decimal TotalCost => (CheckOutDate - CheckInDate).Days * Room.NightlyRate;

        internal Reservation(
            int reservationId, Guest guest, Room room,
            DateTime checkInDate, DateTime checkOutDate)
        {
            if (reservationId <= 0)
                throw new ArgumentException("Reservation ID must be positive");

            if (guest == null)
                throw new ArgumentNullException(nameof(guest));

            if (room == null)
                throw new ArgumentNullException(nameof(room));

            if (checkOutDate.Date <= checkInDate.Date)
                throw new ArgumentException("Check-out must be after check-in");

            if (usedReservationIds.Contains(reservationId))
                throw new ArgumentException("Reservation ID already exists");

            ReservationId = reservationId;
            Guest = guest;
            Room = room;
            CheckInDate = checkInDate.Date;
            CheckOutDate = checkOutDate.Date;
            Status = ReservationStatus.Pending;

            room.RegisterReservation(this);
            usedReservationIds.Add(reservationId);
        }

        public void Confirm()
        {
            if (Status != ReservationStatus.Pending)
                throw new InvalidOperationException("Only pending reservations can be confirmed");

            Status = ReservationStatus.Confirmed;
        }

        public void CheckIn()
        {
            if (Status != ReservationStatus.Confirmed)
                throw new InvalidOperationException("Reservation must be confirmed before check-in");

            Status = ReservationStatus.CheckedIn;
        }

        public void CheckOut()
        {
            if (Status != ReservationStatus.CheckedIn)
                throw new InvalidOperationException("Guest must be checked in before check-out");

            Status = ReservationStatus.CheckedOut;
        }

        public void Cancel()
        {
            if (Status != ReservationStatus.Pending && Status != ReservationStatus.Confirmed)
                throw new InvalidOperationException("Cannot cancel this reservation");

            Status = ReservationStatus.Cancelled;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"Reservation ID: {ReservationId}");
            Console.WriteLine($"Guest: {Guest.FullName}");
            Console.WriteLine($"Room: {Room.RoomNumber}");
            Console.WriteLine($"Check-in: {CheckInDate:yyyy-MM-dd}");
            Console.WriteLine($"Check-out: {CheckOutDate:yyyy-MM-dd}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine($"Total Cost: {TotalCost:F2}");
            Console.WriteLine("----------------------------");
        }
    }
}
