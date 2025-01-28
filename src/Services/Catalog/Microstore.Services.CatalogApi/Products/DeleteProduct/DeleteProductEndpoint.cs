namespace Microstore.Services.CatalogApi.Products.GetProductById;

//public record DeleteProductRequest();
public record DeleteProductResponse(Product Product);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
        {
            DeleteProductResult result = await sender.Send(new DeleteProductCommand(id));
            DeleteProductResponse response = result.Adapt<DeleteProductResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteProducts")
        .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete product")
        .WithDescription("Delete product");
    }
}
