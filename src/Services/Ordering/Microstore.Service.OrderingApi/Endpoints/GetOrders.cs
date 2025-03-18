namespace Microstore.Service.OrderingApi.Endpoints;

//public record class GetOrdersResponse(PaginationRequest PaginationRequest);
public record class GetOrdersResponse(PaginatedResult<OrderDto> Orders);
public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async([AsParameters] PaginationRequest request, ISender sender) =>
        {
            GetOrdersQuery query = new(request);
            GetOrdersResult result = await sender.Send(query);
            GetOrdersResponse response = result.Adapt<GetOrdersResponse>();
            return Results.Ok(response);
        })
        .WithName("GetOrders")
        .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get orders")
        .WithDescription("Get orders");
    }
}
