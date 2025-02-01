using BuildingBlocks.Exceptions;

namespace Microstore.Service.BasketApi.Exceptions;

[Serializable]
internal class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string userName) : base("Basket", userName)
    { }
}