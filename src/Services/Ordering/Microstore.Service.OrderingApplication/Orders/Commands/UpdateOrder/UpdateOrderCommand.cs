namespace Microstore.Service.OrderingApplication.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand (OrderDto Order) : ICommand<UpdateOrderResult>;
public record UpdateOrderResult(bool IsSuccess);
