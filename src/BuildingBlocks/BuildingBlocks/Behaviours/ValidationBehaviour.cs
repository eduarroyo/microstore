﻿using BuildingBlocks.CQRS;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace BuildingBlocks.Behaviours;

public class ValidationBehaviour<TRequest, TResponse>
    (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationContext<TRequest> context = new(request);
        ValidationResult[] validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults
            .Where(request => request.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if(failures.Any())
        {
            throw new ValidationException(failures);
        }

        return await next();

    }
}
