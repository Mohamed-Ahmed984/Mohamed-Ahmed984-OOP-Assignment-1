namespace Part1_ProceduralToOOP
{
    public class Order
    {
        private readonly List<OrderLine> lines = new();

        public int Id { get; }
        public Customer Customer { get; }
        public DateTime Date { get; }
        public bool IsPaid { get; private set; }

        public Order(int id, Customer customer, DateTime date)
        {
            if (id <= 0)
                throw new ArgumentException("Order ID must be positive");

            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            if (date == default)
                throw new ArgumentException("Order date is required");

            Id = id;
            Customer = customer;
            Date = date.Date;
        }

        public void AddLine(Product product, int quantity)
        {
            if (IsPaid)
                throw new InvalidOperationException("Cannot change a paid order");

            if (lines.Count >= 20)
                throw new InvalidOperationException("Order has too many lines");

            if (product == null)
                throw new ArgumentNullException(nameof(product));

            OrderLine line = new OrderLine(product, quantity);
            product.ReduceStock(quantity);
            lines.Add(line);
        }

        public decimal CalculateTotal()
        {
            decimal total = 0;

            foreach (OrderLine line in lines)
                total += line.LineTotal;

            if (Customer.IsVip)
                total *= 0.90m;

            return total;
        }

        public void MarkPaid()
        {
            if (lines.Count == 0)
                throw new InvalidOperationException("Cannot pay an empty order");

            IsPaid = true;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"=== ORDER #{Id} ===");
            Console.WriteLine($"Date: {Date:yyyy-MM-dd}");
            Console.WriteLine($"Customer: {Customer.Name} (#{Customer.Id})");
            Console.WriteLine($"Paid: {(IsPaid ? "Yes" : "No")}");
            Console.WriteLine("Lines:");

            foreach (OrderLine line in lines)
                line.PrintDetails();

            Console.WriteLine($"TOTAL: {CalculateTotal():F2}");
            Console.WriteLine("-----------------------");
        }
    }
}
