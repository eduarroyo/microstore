namespace Microstore.Service.OrderingDomain.ValueObjects;

public record ProductId
{
    public Guid Value { get; }

    private ProductId(Guid value) => Value = value;

    public static ProductId Of(Guid value)
    {
        //ArgumentNullException.ThrowIfNull(value, nameof(value)); // no-op: value can never be null.
        if (value == Guid.Empty)
        {
            throw new DomainException("ProductId cannot be empty");
        }
        return new ProductId(value);
    }
}
