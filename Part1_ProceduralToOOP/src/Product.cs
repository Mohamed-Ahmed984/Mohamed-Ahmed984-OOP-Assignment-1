namespace Part1_ProceduralToOOP
{
    public class Product
    {
        public int Id { get; }
        public string Name { get; set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        public Product(int id, string name, decimal price, int stock)
        {
            if (id <= 0)
                throw new ArgumentException("Product ID must be positive");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");

            if (price <= 0)
                throw new ArgumentException("Price must be positive");

            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative");

            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }

        public void ReduceStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive");

            if (quantity > Stock)
                throw new InvalidOperationException("Not enough stock");

            Stock -= quantity;
        }

        public void AddStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive");

            Stock += quantity;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Price: {Price:F2}");
            Console.WriteLine($"Stock: {Stock}");
            Console.WriteLine("-----------------------");
        }
    }
}
