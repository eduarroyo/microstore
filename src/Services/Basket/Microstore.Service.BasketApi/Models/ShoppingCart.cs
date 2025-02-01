using Marten.Schema;

namespace Microstore.Service.BasketApi.Models;

public class ShoppingCart
{
    public string UserName { get; private set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
    
    public ShoppingCart(string userName):this()
    {
        this.UserName = userName;
    }

    public ShoppingCart()
    {
    }
}