using System.Globalization;

namespace Part1_ProceduralToOOP
{
    internal class Program
    {
        static void Main()
        {
            OrderSystem system = new OrderSystem();
            SeedSampleData(system);
            RunDemoScenario(system);

            system.PrintCustomers();
            system.PrintProducts();
            system.PrintAllOrders();
            Console.WriteLine($"Paid sales total after demo: {system.TotalSalesPaidOnly():F2}");

            RunMenu(system);
        }

        static void SeedSampleData(OrderSystem system)
        {
            system.AddCustomer(1, "Mona Ali", "mona@example.com", "Cairo", true);
            system.AddCustomer(2, "Omar Hassan", "omar@example.com", "Alexandria");
            system.AddCustomer(3, "Sara Nabil", "sara@example.com", "Giza");

            system.AddProduct(101, "USB Cable", 50m, 100);
            system.AddProduct(102, "Wireless Mouse", 250m, 40);
            system.AddProduct(103, "Mechanical Keyboard", 1200m, 15);
            system.AddProduct(104, "Laptop Stand", 400m, 25);
        }

        static void RunDemoScenario(OrderSystem system)
        {
            Order first = system.CreateOrder(1001, 1, new DateTime(2026, 9, 15));
            first.AddLine(GetProduct(system, 101), 2);
            first.AddLine(GetProduct(system, 102), 1);
            first.MarkPaid();

            Order second = system.CreateOrder(1002, 2, new DateTime(2026, 9, 15));
            second.AddLine(GetProduct(system, 103), 1);
            second.AddLine(GetProduct(system, 104), 1);

            Order third = system.CreateOrder(1003, 3, new DateTime(2026, 9, 16));
            third.AddLine(GetProduct(system, 101), 5);
            third.MarkPaid();
        }

        static void RunMenu(OrderSystem system)
        {
            while (true)
            {
                Console.WriteLine("\n---------- MENU ----------");
                Console.WriteLine("1) Print customers");
                Console.WriteLine("2) Print products");
                Console.WriteLine("3) Print all orders");
                Console.WriteLine("4) Print one order by ID");
                Console.WriteLine("5) Create order");
                Console.WriteLine("6) Add line to order");
                Console.WriteLine("7) Mark order paid");
                Console.WriteLine("8) Show paid sales total");
                Console.WriteLine("9) Add customer");
                Console.WriteLine("10) Add product");
                Console.WriteLine("0) Exit");
                Console.Write("Choice: ");

                string? choice = Console.ReadLine();

                if (choice == null || choice == "0")
                {
                    Console.WriteLine("Bye.");
                    return;
                }

                try
                {
                    switch (choice)
                    {
                        case "1":
                            system.PrintCustomers();
                            break;
                        case "2":
                            system.PrintProducts();
                            break;
                        case "3":
                            system.PrintAllOrders();
                            break;
                        case "4":
                            GetOrder(system, ReadInt("Order ID: ")).PrintDetails();
                            break;
                        case "5":
                            int orderId = ReadInt("Order ID: ");
                            int customerId = ReadInt("Customer ID: ");
                            string dateText = ReadText("Date (yyyy-MM-dd): ");

                            if (!DateTime.TryParseExact(dateText, "yyyy-MM-dd",
                                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                                throw new ArgumentException("Date must use yyyy-MM-dd");

                            system.CreateOrder(orderId, customerId, date);
                            break;
                        case "6":
                            Order order = GetOrder(system, ReadInt("Order ID: "));
                            Product product = GetProduct(system, ReadInt("Product ID: "));
                            order.AddLine(product, ReadInt("Quantity: "));
                            break;
                        case "7":
                            GetOrder(system, ReadInt("Order ID: ")).MarkPaid();
                            break;
                        case "8":
                            Console.WriteLine($"Paid sales total: {system.TotalSalesPaidOnly():F2}");
                            break;
                        case "9":
                            int id = ReadInt("Customer ID: ");
                            string name = ReadText("Name: ");
                            string email = ReadText("Email: ");
                            string city = ReadText("City: ");
                            string vip = ReadText("VIP (yes/no): ");

                            if (vip != "yes" && vip != "no")
                                throw new ArgumentException("VIP must be yes or no");

                            system.AddCustomer(id, name, email, city, vip == "yes");
                            break;
                        case "10":
                            int productId = ReadInt("Product ID: ");
                            string productName = ReadText("Name: ");
                            string priceText = ReadText("Price: ");

                            if (!decimal.TryParse(priceText, NumberStyles.Number,
                                CultureInfo.InvariantCulture, out decimal price))
                                throw new ArgumentException("Enter a valid price");

                            system.AddProduct(productId, productName, price, ReadInt("Stock: "));
                            break;
                        default:
                            Console.WriteLine("Unknown choice.");
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
                catch (EndOfStreamException)
                {
                    return;
                }
            }
        }

        static string ReadText(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine()?.Trim() ?? throw new EndOfStreamException();
        }

        static int ReadInt(string prompt)
        {
            if (!int.TryParse(ReadText(prompt), out int value))
                throw new ArgumentException("Enter a valid whole number");

            return value;
        }

        static Product GetProduct(OrderSystem system, int id)
        {
            return system.FindProductById(id) ?? throw new ArgumentException("Product ID not found");
        }

        static Order GetOrder(OrderSystem system, int id)
        {
            return system.FindOrderById(id) ?? throw new ArgumentException("Order ID not found");
        }
    }
}
