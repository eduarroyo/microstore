namespace Microstore.Service.OrderingApplication.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid OrderId);
