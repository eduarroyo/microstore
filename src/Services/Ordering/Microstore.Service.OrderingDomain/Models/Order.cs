﻿namespace Microstore.Service.OrderingDomain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; set; } = default!;
    public Address ShippingAddress { get; set; } = default!;
    public Address BillingAddress { get; set; } = default!;
    public Payment Payment { get; set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice
    {
        get => _orderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }

    public static Order Create
    (
        OrderId orderId,
        CustomerId customerId,
        OrderName orderName,
        Address shippingAddress,
        Address billingAddress,
        Payment payment
    )
    {
        Order order = new Order
        {
            Id = orderId,
            CustomerId = customerId,
            OrderName = orderName, 
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            Status = OrderStatus.Pending
        };
        
        order.AddDomainEvent(new OrderCreatedEvent(order));

        return order;
    }

    public void Update
    (
        OrderName orderName, 
        Address shippingAddress, 
        Address billingAddress, 
        Payment payment, 
        OrderStatus orderStatus
    )
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = orderStatus;

        AddDomainEvent(new OrderUpdatedEvent(this));
    }

    public void Add(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var orderItem = new OrderItem(Id, productId, quantity, price);
        _orderItems.Add(orderItem);
    }

    public void Remove(ProductId productId)
    {
        var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
        if (orderItem != null)
        {
            _orderItems.Remove(orderItem);
        }
    }
}