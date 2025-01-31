namespace Microstore.Services.CatalogApi.Exceptions;

[Serializable]
internal class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid id): base("Product", id)
    {
    }
}