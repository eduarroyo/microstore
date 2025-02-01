using Microstore.Service.BasketApi.Data;

namespace Microstore.Service.BasketApi.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);

public class GetBasketHandler
    (IBasketRepository Repository)
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        // TODO: get basket from database
        ShoppingCart shoppingCart = await Repository.GetBasket(request.UserName, cancellationToken);

        return new GetBasketResult(shoppingCart);
    }
}
