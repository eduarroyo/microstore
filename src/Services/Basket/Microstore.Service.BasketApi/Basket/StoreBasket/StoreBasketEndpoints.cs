namespace Microstore.Service.BasketApi.Basket.GetBasket;

public record StoreBasketRequest(ShoppingCart Cart);

public record StoreBasketResponse(string UserName);

public class StoreBasketEndpoints: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
        {
            StoreBasketCommand command = request.Adapt<StoreBasketCommand>();
            StoreBasketResult result = await sender.Send(command);
            StoreBasketResponse response = result.Adapt<StoreBasketResponse>();
            return Results.Created($"/basket/{response.UserName}", response);
        })
        .WithName("StoreBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store basket")
        .WithDescription("Store basket");
    }
}