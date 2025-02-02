using System.Text.Json;

namespace Microstore.Service.BasketApi.Data;

public class CachedBasketRepository
    (IBasketRepository Repository, IDistributedCache Cache)
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        string? cachedJsonBasket = await Cache.GetStringAsync(userName, cancellationToken);
        if(cachedJsonBasket is not null)
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedJsonBasket)!;
        }
        
        ShoppingCart basket = await Repository.GetBasket(userName, cancellationToken);
        await Cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await Repository.StoreBasket(basket, cancellationToken);
        await Cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        if(!await Repository.DeleteBasket(userName, cancellationToken))
        {
            return false;
        }
        await Cache.RemoveAsync(userName, cancellationToken);
        return true;
    }
}