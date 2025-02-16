﻿namespace Microstore.Service.OrderingDomain.ValueObjects;

public record Payment
(
    string? CardName,
    string CardNumber,
    string Expiration,
    string CVV,
    string PaymentMethod
);