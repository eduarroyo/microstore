namespace Microstore.Service.OrderingApplication.Orders.Commands.DeleteOrder;
public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;
public record class DeleteOrderResult(bool IsSuccess);
