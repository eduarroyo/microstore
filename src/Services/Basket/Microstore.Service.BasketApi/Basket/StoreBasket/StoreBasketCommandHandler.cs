namespace Microstore.Service.BasketApi.Basket.GetBasket;

public record StoreBasketCommand(ShoppingCart Cart): ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandHandler
    ()
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        // TODO: store basket in database (use Marten upsert).
        // TODO: update cache

        return new StoreBasketResult("Captain Haddock");
    }
}
