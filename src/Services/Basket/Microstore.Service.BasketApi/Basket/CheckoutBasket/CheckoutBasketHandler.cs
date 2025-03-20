namespace Microstore.Service.BasketApi.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);


public class CheckoutBasketHandler
    (IBasketRepository Repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        // Get existing basket with total price
        ShoppingCart basket = await Repository.GetBasket(command.BasketCheckoutDto.UserName);
        if (basket == null)
        {
            return new CheckoutBasketResult(false);
        }

        // Set total price on basketcheckout event message
        BasketCheckoutEvent eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        // Send basket checkout event to rabbitMq using masstransit
        await publishEndpoint.Publish(basket.Adapt<BasketCheckoutEvent>());

        // delete the basket
        _ = await Repository.DeleteBasket(command.BasketCheckoutDto.UserName);

        return new CheckoutBasketResult(true);
    }
}
