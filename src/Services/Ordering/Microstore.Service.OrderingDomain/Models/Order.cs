namespace Microstore.Service.OrderingDomain.Models;

public class Order : Aggregate<Guid>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public Guid CustomerId { get; private set; } = default!;
    public string OrderName { get; set; } = default!;
    public Address ShippingAddress { get; set; } = default!;
    public Address BillingAddress { get; set; } = default!;
    public Payment Payment { get; set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice
    {
        get => _orderItems.Sum(x => x.Price * x.Quantity);
    }
}
