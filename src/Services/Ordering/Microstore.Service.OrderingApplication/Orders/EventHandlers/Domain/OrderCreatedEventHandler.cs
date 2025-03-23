namespace Microstore.Service.OrderingApplication.Orders.EventHandlers.Domain;
public class OrderCreatedEventHandler
    (IPublishEndpoint publisher, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
        if (await featureManager.IsEnabledAsync("OrderFullfilment"))
        {
            OrderDto orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();
            await publisher.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }
    }
}
