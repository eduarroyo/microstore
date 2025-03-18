namespace Microstore.Service.OrderingApi.Endpoints;

//public record DeleteOrderRequest(Guid OrderId);
public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/order/{orderId:guid}", async (Guid orderId, ISender sender) =>
        {
            DeleteOrderResult result = await sender.Send(new DeleteOrderCommand(orderId));
            DeleteOrderResponse response = result.Adapt<DeleteOrderResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteOrder")
        .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete order")
        .WithDescription("Deletes an order by ID.");
    }
}
