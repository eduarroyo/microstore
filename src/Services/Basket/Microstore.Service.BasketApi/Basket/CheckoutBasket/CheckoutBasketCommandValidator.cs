
namespace Microstore.Service.BasketApi.Basket.CheckoutBasket;

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto cannot be null");
        RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName cannot be empty");
    }
}
