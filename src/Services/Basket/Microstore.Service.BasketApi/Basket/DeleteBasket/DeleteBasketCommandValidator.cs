﻿namespace Microstore.Service.BasketApi.Basket.GetBasket;

public class DeleteBasketCommandValidator: AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
    }
}
