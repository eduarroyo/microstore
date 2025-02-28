﻿namespace Microstore.Service.OrderingApplication.Dtos;

public record AddressDto
(
    string FirstName,
    string LastName,
    string EmailAddress,
    string AddressLine,
    string Country,
    string State,
    string ZipCode
);
