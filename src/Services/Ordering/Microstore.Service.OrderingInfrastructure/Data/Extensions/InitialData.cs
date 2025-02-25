using Microstore.Service.OrderingDomain.Enums;

namespace Microstore.Service.OrderingInfrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Customer> Customers =>
        new List<Customer>
        {
            Customer.Create(CustomerId.Of(new Guid("4ee0ed9b-67f8-438b-9d53-70d8a951dbbd")), "Sonia", "sonia@fakemail.com"),
            Customer.Create(CustomerId.Of(new Guid("042e70cf-1f70-4238-bbe6-a5c36b2ff391")), "John", "john@fakemail.com")
        };

    public static IEnumerable<Product> Products =>
        new List<Product>
        {
            Product.Create(ProductId.Of(new Guid("0bad0c1e-fdc0-42ce-a043-958e9ff57c28")), "IPhone X", 500),
            Product.Create(ProductId.Of(new Guid("f85d7b54-64c6-4933-9b96-58e6e3b5c66d")), "Samsung 10", 400),
            Product.Create(ProductId.Of(new Guid("9b683925-5dcc-4804-abe8-39a5dab72900")), "Huawei Plus", 650),
            Product.Create(ProductId.Of(new Guid("636c97a1-499d-4a6f-bde9-0c6d380b76fb")), "Xiaomi Mi", 450)
        };

    public static IEnumerable<Order> OrdersWithItems 
    {
        get
        {
            Address addr1 = Address.Of("Sonia", "Thompson", "sonia@fakemail.com", "Fake Street 123", "USA", "Springfield", "12345");
            Address addr2 = Address.Of("John", "McFake", "john@fakemail.com", "Fake Avenue 333", "Mexico", "Chihuahua", "43243");
            Payment payment1 = Payment.Of("Sonia", "11111111111", "12/28", "111", 1);
            Payment payment2 = Payment.Of("John",  "22222222222", "6/29", "222", 2);
            Order order1 = Order.Create
            (
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("4ee0ed9b-67f8-438b-9d53-70d8a951dbbd")),
                OrderName.Of("ORD_1"),
                shippingAddress: addr1,
                billingAddress: addr1,
                payment1,
                OrderStatus.Completed
            );
            order1.Add(ProductId.Of(new Guid("0bad0c1e-fdc0-42ce-a043-958e9ff57c28")), 2, 500);
            order1.Add(ProductId.Of(new Guid("f85d7b54-64c6-4933-9b96-58e6e3b5c66d")), 1, 400);

            Order order2 = Order.Create
            (
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("042e70cf-1f70-4238-bbe6-a5c36b2ff391")),
                OrderName.Of("ORD_2"),
                shippingAddress: addr2,
                billingAddress: addr2,
                payment2,
                OrderStatus.Pending
            );
            order2.Add(ProductId.Of(new Guid("9b683925-5dcc-4804-abe8-39a5dab72900")), 1, 650);
            order2.Add(ProductId.Of(new Guid("636c97a1-499d-4a6f-bde9-0c6d380b76fb")), 2, 450);

            return new List<Order> { order1, order2 };
        }
    }
}