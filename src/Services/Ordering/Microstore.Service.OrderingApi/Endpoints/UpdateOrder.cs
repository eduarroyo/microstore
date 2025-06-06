﻿namespace Microstore.Service.OrderingApi.Endpoints;

public record UpdateOrderRequest(OrderDto Order);
public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async(UpdateOrderRequest request, ISender sender) =>
        {
            UpdateOrderCommand command = request.Adapt<UpdateOrderCommand>();
            UpdateOrderResult result = await sender.Send(command);
            UpdateOrderResponse response = result.Adapt<UpdateOrderResponse>();
            return Results.Ok(response);
        })
        .WithName("UpdateOrder")
        .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update order")
        .WithDescription("Update order");

    }
}
