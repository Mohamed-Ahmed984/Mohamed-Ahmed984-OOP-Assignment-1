namespace HotelSystem
{
    public enum RoomType
    {
        Single,
        Double,
        Suite
    }

    public class Room
    {
        private static readonly HashSet<int> usedRoomNumbers = new();
        private readonly List<Reservation> reservations = new();

        public int RoomNumber { get; }
        public RoomType RoomType { get; }
        public decimal NightlyRate { get; private set; }
        public bool IsUnderMaintenance { get; private set; }

        public Room(int number, RoomType type, decimal price)
        {
            if (number <= 0)
                throw new ArgumentException("Room number must be positive");

            if (!Enum.IsDefined(type))
                throw new ArgumentException("Room type must be Single, Double or Suite");

            if (price <= 0)
                throw new ArgumentException("Price must be positive");

            if (usedRoomNumbers.Contains(number))
                throw new ArgumentException("Room number already exists");

            RoomNumber = number;
            RoomType = type;
            NightlyRate = price;
            usedRoomNumbers.Add(number);
        }

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("Price must be positive");

            NightlyRate = newPrice;
        }

        public void StartMaintenance()
        {
            IsUnderMaintenance = true;
        }

        public void EndMaintenance()
        {
            IsUnderMaintenance = false;
        }

        internal void RegisterReservation(Reservation reservation)
        {
            if (IsUnderMaintenance)
                throw new InvalidOperationException("Cannot book a room under maintenance");

            foreach (Reservation existing in reservations)
            {
                bool active = existing.Status != ReservationStatus.Cancelled &&
                              existing.Status != ReservationStatus.CheckedOut;

                bool overlaps = reservation.CheckInDate < existing.CheckOutDate &&
                                existing.CheckInDate < reservation.CheckOutDate;

                if (active && overlaps)
                    throw new InvalidOperationException("Room already has an overlapping reservation");
            }

            reservations.Add(reservation);
        }
    }
}
