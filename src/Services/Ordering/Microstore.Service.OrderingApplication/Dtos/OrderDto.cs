﻿using Microstore.Service.OrderingDomain.Enums;

namespace Microstore.Service.OrderingApplication.Dtos;

public record OrderDto
(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    OrderStatus Status,
    List<OrderItemDto> OrderItems
);
