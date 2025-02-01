using Microstore.Service.BasketApi.Data;

namespace Microstore.Service.BasketApi.Basket.GetBasket;

public record StoreBasketCommand(ShoppingCart Cart): ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandHandler
    (IBasketRepository Repository)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        // Store basket in database (use Marten upsert).
        ShoppingCart updatedCart = await Repository.StoreBasket(cart, cancellationToken);
        // TODO: update cache

        return new StoreBasketResult(updatedCart.UserName);
    }
}
