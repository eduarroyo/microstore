namespace Microstore.Service.OrderingApi.Endpoints;

// public record GetOrdersByNameRequest(string Name);
public record GetOrdersByNameResponse(List<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/order/{orderName:required}", async (string orderName, ISender sender) =>
        {
            GetOrdersByNameResult result = await sender.Send(new GetOrdersByNameQuery(orderName));
            GetOrdersByNameResponse response = result.Adapt<GetOrdersByNameResponse>();
            return Results.Ok(response);
        })
        .WithName("GetOrdersByName")
        .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get orders by name")
        .WithDescription("Get orders by name");
    }
}
