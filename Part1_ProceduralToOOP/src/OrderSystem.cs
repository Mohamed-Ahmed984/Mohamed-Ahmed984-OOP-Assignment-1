namespace Part1_ProceduralToOOP
{
    public class OrderSystem
    {
        private readonly List<Customer> customers = new();
        private readonly List<Product> products = new();
        private readonly List<Order> orders = new();

        public Customer AddCustomer(int id, string name, string email, string city, bool isVip = false)
        {
            if (FindCustomerById(id) != null)
                throw new ArgumentException("Customer ID already exists");

            Customer customer = new Customer(id, name, email, city);

            if (isVip)
                customer.UpgradeVip();

            customers.Add(customer);
            return customer;
        }

        public Product AddProduct(int id, string name, decimal price, int stock)
        {
            if (FindProductById(id) != null)
                throw new ArgumentException("Product ID already exists");

            Product product = new Product(id, name, price, stock);
            products.Add(product);
            return product;
        }

        public Order CreateOrder(int id, int customerId, DateTime date)
        {
            if (FindOrderById(id) != null)
                throw new ArgumentException("Order ID already exists");

            Customer customer = FindCustomerById(customerId)
                ?? throw new ArgumentException("Customer ID not found");

            Order order = new Order(id, customer, date);
            orders.Add(order);
            return order;
        }

        public Customer? FindCustomerById(int id)
        {
            foreach (Customer customer in customers)
                if (customer.Id == id)
                    return customer;

            return null;
        }

        public Product? FindProductById(int id)
        {
            foreach (Product product in products)
                if (product.Id == id)
                    return product;

            return null;
        }

        public Order? FindOrderById(int id)
        {
            foreach (Order order in orders)
                if (order.Id == id)
                    return order;

            return null;
        }

        public void PrintCustomers()
        {
            foreach (Customer customer in customers)
                customer.PrintDetails();
        }

        public void PrintProducts()
        {
            foreach (Product product in products)
                product.PrintDetails();
        }

        public void PrintAllOrders()
        {
            foreach (Order order in orders)
                order.PrintDetails();
        }

        public decimal TotalSalesPaidOnly()
        {
            decimal total = 0;

            foreach (Order order in orders)
                if (order.IsPaid)
                    total += order.CalculateTotal();

            return total;
        }
    }
}
