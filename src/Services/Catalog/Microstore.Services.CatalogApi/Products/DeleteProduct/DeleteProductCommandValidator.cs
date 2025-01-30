namespace Microstore.Services.CatalogApi.Products.GetProductById;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Product ID is required");
    }
}
