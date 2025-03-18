namespace Microstore.Service.OrderingApplication.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid id): base("Order", id)
    {
    }
}