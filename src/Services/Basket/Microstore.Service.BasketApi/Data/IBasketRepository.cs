﻿namespace Microstore.Service.BasketApi.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default);
    Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
}
