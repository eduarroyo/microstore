using BuildingBlocks.Messaging.Events;
using Microstore.Service.OrderingApplication.Orders.Commands.CreateOrder;

namespace Microstore.Service.OrderingApplication.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler
    (ISender sender, ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.Id);
        CreateOrderCommand command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        AddressDto addressDto = new
        (
            message.FirstName,
            message.LastName,
            message.EmailAddress,
            message.AddressLine,
            message.Country,
            message.State,
            message.ZipCode
        );

        PaymentDto paymentDto = new
        (
            message.CardName,
            message.CardNumber,
            message.Expiration,
            message.CVV,
            message.PaymentMethod
        );

        Guid orderId = Guid.NewGuid();

        OrderDto orderDto = new
        (
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.UserName,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            Status: OrderingDomain.Enums.OrderStatus.Pending,
            OrderItems:
            [
                new OrderItemDto(orderId, new Guid("0bad0c1e-fdc0-42ce-a043-958e9ff57c28"), 2, 500),
                new OrderItemDto(orderId, new Guid("f85d7b54-64c6-4933-9b96-58e6e3b5c66d"), 1, 400)
            ]
        );

        return new CreateOrderCommand(orderDto);
    }
}
