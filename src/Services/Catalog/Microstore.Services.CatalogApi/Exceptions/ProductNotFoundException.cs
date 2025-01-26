namespace Microstore.Services.CatalogApi.Exceptions;

[Serializable]
internal class ProductNotFoundException : Exception
{
    public ProductNotFoundException(): base("Product not found")
    {
    }
}