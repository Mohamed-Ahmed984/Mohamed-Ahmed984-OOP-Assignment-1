using System;


namespace Task32
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string BillingStreet { get; set; } = "";
        public string BillingCity { get; set; } = "";
        public string BillingState { get; set; } = "";
        public string BillingZip { get; set; } = "";
        public string BillingCountry { get; set; } = "";
        public string ShippingStreet { get; set; } = "";
        public string ShippingCity { get; set; } = "";
        public string ShippingState { get; set; } = "";
        public string ShippingZip { get; set; } = "";
        public string ShippingCountry { get; set; } = "";
        public DateTime Date { get; set; }
        public string Payment { get; set; } = "";
        public string Currency { get; set; } = "";
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total => Subtotal - Discount + Tax;
    }

    public class InvoiceBuilder
    {
        private readonly Invoice invoice = new Invoice();

        public InvoiceBuilder Customer(int id, string name, string email, string phone = "")
        {
            invoice.Id = id; invoice.Name = name; invoice.Email = email; invoice.Phone = phone;
            return this;
        }
        public InvoiceBuilder Billing(string street, string city, string state, string zip, string country)
        {
            invoice.BillingStreet = street; invoice.BillingCity = city;
            invoice.BillingState = state; invoice.BillingZip = zip; invoice.BillingCountry = country;
            return this;
        }
        public InvoiceBuilder Shipping(string street, string city, string state, string zip, string country)
        {
            invoice.ShippingStreet = street; invoice.ShippingCity = city;
            invoice.ShippingState = state; invoice.ShippingZip = zip; invoice.ShippingCountry = country;
            return this;
        }
        public InvoiceBuilder Order(DateTime date, string payment, string currency,
                                    decimal subtotal, decimal discount = 0, decimal tax = 0)
        {
            invoice.Date = date; invoice.Payment = payment; invoice.Currency = currency;
            invoice.Subtotal = subtotal; invoice.Discount = discount; invoice.Tax = tax;
            return this;
        }
        public Invoice Build()
        {
            if (invoice.Id <= 0 || string.IsNullOrWhiteSpace(invoice.Name) ||
                string.IsNullOrWhiteSpace(invoice.Email) ||
                string.IsNullOrWhiteSpace(invoice.BillingStreet) ||
                string.IsNullOrWhiteSpace(invoice.BillingCity) ||
                string.IsNullOrWhiteSpace(invoice.BillingState) ||
                string.IsNullOrWhiteSpace(invoice.BillingZip) ||
                string.IsNullOrWhiteSpace(invoice.BillingCountry) ||
                string.IsNullOrWhiteSpace(invoice.ShippingStreet) ||
                string.IsNullOrWhiteSpace(invoice.ShippingCity) ||
                string.IsNullOrWhiteSpace(invoice.ShippingState) ||
                string.IsNullOrWhiteSpace(invoice.ShippingZip) ||
                string.IsNullOrWhiteSpace(invoice.ShippingCountry) ||
                invoice.Date == default || string.IsNullOrWhiteSpace(invoice.Payment) ||
                string.IsNullOrWhiteSpace(invoice.Currency) || invoice.Subtotal < 0 ||
                invoice.Discount < 0 || invoice.Discount > invoice.Subtotal || invoice.Tax < 0)
                throw new ArgumentException("Provide customer details, complete addresses, date, payment and currency. Amounts must be non-negative and discount cannot exceed subtotal.");
            return invoice;
        }
    }
}

namespace Task33
{
    public class Address
    {
        public string Street { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string Zip { get; set; } = "";
        public string Country { get; set; } = "";

        internal void Validate()
        {
            if (string.IsNullOrWhiteSpace(Street) ||
                string.IsNullOrWhiteSpace(City) ||
                string.IsNullOrWhiteSpace(State) ||
                string.IsNullOrWhiteSpace(Zip) ||
                string.IsNullOrWhiteSpace(Country))
                throw new ArgumentException("Address requires street, city, state, ZIP code and country");
        }
    }
    public class AddressBuilder
    {
        private readonly Address address = new Address();
        public AddressBuilder Set(string street, string city, string state, string zip, string country)
        {
            address.Street = street; address.City = city; address.State = state;
            address.Zip = zip; address.Country = country;
            return this;
        }
        public Address Build()
        {
            address.Validate();
            return address;
        }
    }

    public class OrderInfo
    {
        public DateTime Date { get; set; }
        public string Payment { get; set; } = "";
        public string Currency { get; set; } = "";
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total => Subtotal - Discount + Tax;

        internal void Validate()
        {
            if (Date == default || string.IsNullOrWhiteSpace(Payment) ||
                string.IsNullOrWhiteSpace(Currency))
                throw new ArgumentException("Order requires a date, payment method and currency");

            if (Subtotal < 0 || Discount < 0 || Discount > Subtotal || Tax < 0)
                throw new ArgumentException("Amounts must be non-negative and discount cannot exceed subtotal");
        }
    }
    public class OrderBuilder
    {
        private readonly OrderInfo order = new OrderInfo();
        public OrderBuilder Set(DateTime date, string payment, string currency, decimal subtotal)
        {
            order.Date = date; order.Payment = payment;
            order.Currency = currency; order.Subtotal = subtotal;
            return this;
        }
        public OrderBuilder Discount(decimal value) { order.Discount = value; return this; }
        public OrderBuilder Tax(decimal value) { order.Tax = value; return this; }
        public OrderInfo Build()
        {
            order.Validate();
            return order;
        }
    }

    public class Invoice
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public Address? Billing { get; set; }
        public Address? Shipping { get; set; }
        public OrderInfo? Order { get; set; }
    }
    public class InvoiceBuilder
    {
        private readonly Invoice invoice = new Invoice();
        public InvoiceBuilder Customer(int id, string name, string email, string phone = "")
        {
            invoice.Id = id; invoice.Name = name;
            invoice.Email = email; invoice.Phone = phone;
            return this;
        }
        public InvoiceBuilder Billing(Address address) { invoice.Billing = address; return this; }
        public InvoiceBuilder Shipping(Address address) { invoice.Shipping = address; return this; }
        public InvoiceBuilder Order(OrderInfo order) { invoice.Order = order; return this; }
        public Invoice Build()
        {
            if (invoice.Id <= 0 || string.IsNullOrWhiteSpace(invoice.Name) ||
                string.IsNullOrWhiteSpace(invoice.Email) || invoice.Billing == null ||
                invoice.Shipping == null || invoice.Order == null)
                throw new ArgumentException("Invoice requires a positive ID, customer name/email, two addresses and order information");

            invoice.Billing.Validate();
            invoice.Shipping.Validate();
            invoice.Order.Validate();
            return invoice;
        }
    }
}

class Program
{
    static void Main()
    {
        var first = new Task32.InvoiceBuilder()
            .Customer(1, "Ahmed", "ahmed@email.com")
            .Billing("Street 1", "Cairo", "Cairo", "12345", "Egypt")
            .Shipping("Street 2", "Giza", "Giza", "54321", "Egypt")
            .Order(DateTime.Today, "Card", "EGP", 1000, 100, 140)
            .Build();
        Console.WriteLine($"Task 3.2 Total: {first.Total}");

        var billing = new Task33.AddressBuilder()
            .Set("Street 1", "Cairo", "Cairo", "12345", "Egypt").Build();
        var shipping = new Task33.AddressBuilder()
            .Set("Street 2", "Giza", "Giza", "54321", "Egypt").Build();
        var order = new Task33.OrderBuilder()
            .Set(DateTime.Today, "Card", "EGP", 1000)
            .Discount(100).Tax(140).Build();
        var second = new Task33.InvoiceBuilder()
            .Customer(2, "Mona", "mona@email.com")
            .Billing(billing).Shipping(shipping).Order(order).Build();
        Console.WriteLine($"Task 3.3 Total: {second.Order!.Total}");
    }
}
