namespace Part1_ProceduralToOOP
{
    public class OrderLine
    {
        public Product Product { get; }
        public int Quantity { get; }
        public decimal UnitPrice { get; }
        public decimal LineTotal => UnitPrice * Quantity;

        public OrderLine(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive");

            Product = product;
            Quantity = quantity;
            UnitPrice = product.Price;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"{Product.Name} x{Quantity} @{UnitPrice:F2} = {LineTotal:F2}");
        }
    }
}
