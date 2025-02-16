namespace Microstore.Service.OrderingDomain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; }

    private OrderItemId(Guid value) => Value = value;

    public static OrderItemId Of(Guid value)
    {
        //ArgumentNullException.ThrowIfNull(value, nameof(value)); // no-op: value can never be null.
        if (value == Guid.Empty)
        {
            throw new DomainException("OrderItemId cannot be empty");
        }
        return new OrderItemId(value);
    }
}