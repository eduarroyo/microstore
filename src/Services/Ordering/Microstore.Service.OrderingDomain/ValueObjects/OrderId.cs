namespace Microstore.Service.OrderingDomain.ValueObjects;

public record OrderId
{
    public Guid Value { get; }

    private OrderId(Guid value) => Value = value;

    public static OrderId Of(Guid value)
    {
        //ArgumentNullException.ThrowIfNull(value, nameof(value)); // no-op: value can never be null.
        if (value == Guid.Empty)
        {
            throw new DomainException("OrderId cannot be empty");
        }
        return new OrderId(value);
    }
}
