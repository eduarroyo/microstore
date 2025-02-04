using Microstore.Service.DiscountGrpc;

namespace Microstore.Service.BasketApi.Basket.GetBasket;

public record StoreBasketCommand(ShoppingCart Cart): ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandHandler
    (IBasketRepository Repository, DiscountProtoService.DiscountProtoServiceClient discountClient)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, discountClient, cancellationToken);
        ShoppingCart updatedCart = await Repository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(updatedCart.UserName);
    }

    private static async Task DeductDiscount
    (
        ShoppingCart cart, 
        DiscountProtoService.DiscountProtoServiceClient discountClient, 
        CancellationToken cancellationToken
    )
    {
        foreach (var item in cart.Items)
        {
            CouponModel coupon = await discountClient.GetDiscountAsync
            (
                new GetDiscountRequest { ProductName = item.ProductName },
                cancellationToken: cancellationToken
            );
            item.Price -= coupon.Amount;
        }
    }
}
