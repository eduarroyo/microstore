namespace Microstore.Service.BasketApi.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);

public class GetBasketHandler
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        // TODO: get basket from database
        // Basket basket = await _repository.GetBasketAsync(request.UserName);

        return new GetBasketResult(new ShoppingCart("Captain Haddock"));
    }
}
