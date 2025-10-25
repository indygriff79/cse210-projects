using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class Address
{
    private string street;
    private string city;
    private string stateProvidence;
    private string country;

    public Address(string street, string city, string stateProvidence, string country)
    {
        this.street = street;
        this.city = city;
        this.stateProvidence = stateProvidence;
        this.country = country;
    }

    public bool IsInUsa()
    {
        return country.ToLower() == "usa";
    }

    public override string ToString()
    {
        return $"{street}\n{city}, {stateProvidence}\n{country}";
    }
}
public class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }
    public bool IsInUsa()
    {
        return address.IsInUsa();
    }
    public string GetAddressString()
    {
        return address.ToString();
    }
    public string GetName()
    {
        return name;
    }
}

public class Product
{
    private string name;
    private string productId;
    private double pricePerUnit;
    private int quantity;

    public Product(string name, string productId, double pricePerUnit, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.pricePerUnit = pricePerUnit;
        this.quantity = quantity;
    }

    public double GetTotalCost()
    {
        return pricePerUnit * quantity;
    }
    public string GetProductInfo()
    {
        return $"{name} (ID: {productId}) - {quantity} @ ${pricePerUnit}";
    }
}

public class Order
{
    private List<Product> products = new List<Product>();
    private Customer customer;

    public Order(Customer customer)
    {
        this.customer = customer;
    }
    public void AddProduct(Product product)
    {
        products.Add(product);
    }
    public double CalculateTotalCost()
    {
        double total = 0;
        foreach (var product in products)
        {
            total += product.GetTotalCost();
        }
        total += customer.IsInUsa() ? 5 : 35; //Shipping cost
        return total;
    }
    public string GetPackingLabel()
    {
        string label = "Packing List:\n";
        foreach (var product in products)
        {
            label += $"- {product.GetProductInfo()}\n";
        }
        return label;
    }
    public string GetShippingLabel()
    {
        return $"Ship to: \n{customer.GetName()}\n{customer.GetAddressString()}";
    }
}

class Program
{
    static void Main()
    {
        Address address1 = new Address("123 Elm St", "Dallas", "TX", "USA");
        Customer customer1 = new Customer("John Smith", address1);
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop", "A001", 999.99, 1));
        order1.AddProduct(new Product("Mouse", "B002", 25.50, 2));

        Address address2 = new Address("10 Downing St", "London", "", "UK");
        Customer customer2 = new Customer("Jane Doe", address2);
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Keyboard", "C003", 49.99, 1));
        order2.AddProduct(new Product("Monitor", "D004", 199.99, 2));

        List<Order> orders = new List<Order> { order1, order2 };

        foreach (var order in orders)
        {
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine($"Total Cost: ${order.CalculateTotalCost():0.00}");
            Console.WriteLine(new string('-', 40));
        }
    }
}
    
