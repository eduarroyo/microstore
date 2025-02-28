namespace Microstore.Service.OrderingApplication.Orders.Commands.UpdateOrder;
public class UpdateOrderHandler
    (IApplicationDbContext dbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        // Update existing order from the command object
        OrderId orderId = OrderId.Of(command.Order.Id);
        Order? order = await dbContext.Orders
            .FindAsync([orderId], cancellationToken);
        if(order is null)
        {
            throw new OrderNotFoundException(command.Order.Id);
        }

        UpdateOrderWithNewValues(order, command.Order);

        // Save changes to database
        await dbContext.SaveChangesAsync(cancellationToken);

        // Return result
        return new UpdateOrderResult(true);
    }

    private void UpdateOrderWithNewValues(Order original, OrderDto orderDto)
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

        original.Update
        (
            OrderName.Of(orderDto.OrderName),
            shippingAddres,
            billingAddres,
            payment,
            orderDto.Status
        );
    }
}
