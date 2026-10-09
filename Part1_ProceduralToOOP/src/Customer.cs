namespace Part1_ProceduralToOOP
{
    public class Customer
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public bool IsVip { get; private set; }

        public Customer(int id, string name, string email, string city)
        {
            if (id <= 0)
                throw new ArgumentException("Customer ID must be positive");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");

            Id = id;
            Name = name;
            Email = email;
            City = city;
        }

        public void UpgradeVip()
        {
            IsVip = true;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"City: {City}");
            Console.WriteLine($"VIP: {(IsVip ? "Yes" : "No")}");
            Console.WriteLine("-----------------------");
        }
    }
}
