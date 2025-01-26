namespace Microstore.Services.CatalogApi.Products.GetProductsByCategory;

//public record GetProductGetProductsByCategoryQueryHandlerRequest();
public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

public class GetProductGetProductsByCategoryQueryHandlerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            GetProductsByCategoryResult result = await sender.Send(new GetProductsByCategoryQuery(category));
            GetProductsByCategoryResponse response = result.Adapt<GetProductsByCategoryResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProductsByCategory")
        .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products By Category")
        .WithDescription("Get products by category");
    }
}
