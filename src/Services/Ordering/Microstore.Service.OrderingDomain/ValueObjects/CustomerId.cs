﻿namespace Microstore.Service.OrderingDomain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; }

    private CustomerId(Guid value) => Value = value;

    public static CustomerId Of(Guid value)
    {
        //ArgumentNullException.ThrowIfNull(value, nameof(value)); // no-op: value can never be null.
        if(value == Guid.Empty)
        {
            throw new DomainException("CustomerId cannot be empty");
        }
        return new CustomerId(value);
    }
}
