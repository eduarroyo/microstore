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

internal class CreateProductCommandHandler(IDocumentSession session)
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

        // 2. Save product entity to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        // 3. Return CreateProductResult with product id
        return new CreateProductResult(product.Id);
    }
}