﻿namespace Microstore.Service.OrderingApplication.Orders.Commands.CreateOrder;

public class CreateOrderHandler
    (IApplicationDbContext dbContext)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle
    (
        CreateOrderCommand command, 
        CancellationToken cancellationToken
    )
    {
        // Create Order entity from command object+
        Order order = CreateNewOrder(command.Order);

        // Save data to database
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        // return result
        return new CreateOrderResult(order.Id.Value);
    }

    private Order CreateNewOrder(OrderDto orderDto)
    {
        Address shippingAddres = Address.Of
        (
            orderDto.ShippingAddress.FirstName,
            orderDto.ShippingAddress.LastName,
            orderDto.ShippingAddress.EmailAddress,
            orderDto.ShippingAddress.AddressLine,
            orderDto.ShippingAddress.Country,
            orderDto.ShippingAddress.State,
            orderDto.ShippingAddress.ZipCode
        );

        Address billingAddres = Address.Of
        (
            orderDto.BillingAddress.FirstName,
            orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.EmailAddress,
            orderDto.BillingAddress.AddressLine,
            orderDto.BillingAddress.Country,
            orderDto.BillingAddress.State,
            orderDto.BillingAddress.ZipCode
        );

        Payment payment = Payment.Of
        (
            orderDto.Payment.CardName,
            orderDto.Payment.CardNumber,
            orderDto.Payment.Expiration,
            orderDto.Payment.Cvv,
            orderDto.Payment.PaymentMethod
        );

        Order newOrder = Order.Create
        (
            OrderId.Of(Guid.NewGuid()),
            CustomerId.Of(orderDto.CustomerId),
            OrderName.Of(orderDto.OrderName),
            shippingAddres,
            billingAddres,
            payment
        );

        foreach (OrderItemDto orderItemDto in orderDto.OrderItems)
        {
            newOrder.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
        }

        return newOrder;
    }
}