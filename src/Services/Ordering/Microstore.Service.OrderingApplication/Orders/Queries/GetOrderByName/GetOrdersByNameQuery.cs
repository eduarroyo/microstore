namespace Microstore.Service.OrderingApplication.Orders.Queries.GetOrderByName;
public record GetOrdersByNameQuery(string Name): IQuery<GetOrdersByNameResult>;
public record class GetOrdersByNameResult(IEnumerable<OrderDto> Orders);
