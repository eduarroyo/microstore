namespace Microstore.Service.OrderingDomain.Events;

public record OrderCreatedEvent(Order Order) : IDomainEvent;