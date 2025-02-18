namespace Microstore.Service.OrderingDomain.Events;

public record OrderUpdatedEvent(Order Order) : IDomainEvent;