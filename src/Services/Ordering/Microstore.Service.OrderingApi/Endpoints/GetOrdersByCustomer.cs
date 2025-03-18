namespace Microstore.Service.OrderingApi.Endpoints;

// public record GetOrdersByCustomerRequest(Guid CustomerId);
public record class GetOrdersByCustomerResponse(List<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId:guid}", async (Guid customerId, ISender sender) =>
        {
            GetOrdersByCustomerQuery query = new GetOrdersByCustomerQuery(customerId);
            GetOrdersByCustomerResult result = await sender.Send(query);
            GetOrdersByCustomerResponse response = result.Adapt<GetOrdersByCustomerResponse>();
            return Results.Ok(response);
        })
        .WithName("GetOrdersByCustomer")
        .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get orders by customer")
        .WithDescription("Get orders by customer");
    }
}
