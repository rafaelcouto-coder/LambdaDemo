using AWSLambda1.Model;
using Refit;

namespace AWSLambda1.Interface;

public interface IFakeStoreApi
{
    [Get("/products")]
    Task<List<Product>> GetProductsAsync();
}
