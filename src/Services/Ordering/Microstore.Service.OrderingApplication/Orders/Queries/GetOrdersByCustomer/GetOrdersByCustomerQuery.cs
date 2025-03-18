namespace Microstore.Service.OrderingApplication.Orders.Queries.GetOrderByCustomer;
public record GetOrdersByCustomerQuery(Guid CustomerId): IQuery<GetOrdersByCustomerResult>;
public record class GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);
