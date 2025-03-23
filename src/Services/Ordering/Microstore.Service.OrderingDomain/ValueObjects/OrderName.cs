namespace Microstore.Service.OrderingDomain.ValueObjects;

public record OrderName
{
    private const int DefaultLength = 3;
    public string Value { get; } = default!;

    private OrderName(string value) => Value = value;

    public static OrderName Of(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));
        //ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength, nameof(value));

        return new OrderName(value);
    }
}
