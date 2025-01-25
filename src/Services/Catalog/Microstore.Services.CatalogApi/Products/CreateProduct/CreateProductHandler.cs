﻿using BuildingBlocks.CQRS;
using Microstore.Services.CatalogApi.Models;

namespace Microstore.Services.CatalogApi.Products.CreateProduct;

public record CreateProductCommand 
(
    string Name,
    List<string> Category,
    string Description, 
    string ImageFile,
    decimal Price
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // 1. Create product entity from command object
        Product product = new()
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // TODO 2. Save product entity to database


        // 3. Return CreateProductResult with product id
        return new CreateProductResult(Guid.NewGuid());
    }
}