﻿namespace Microstore.Service.BasketApi.Basket.GetBasket;

//public record DeleteBasketRequest(string UserName);
public record class DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
        {
            DeleteBasketResult result = await sender.Send(new DeleteBasketCommand(userName));
            DeleteBasketResponse response = result.Adapt<DeleteBasketResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteBasket")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithDescription("Delete basket by user")
        .WithSummary("Delete basket by user");
    }
}