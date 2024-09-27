using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using AWSLambda1.Interface;
using AWSLambda1.Options;
using Microsoft.Extensions.Options;
using Refit;

namespace AWSLambda1;

public class OrderProcessor
{
    private readonly IFakeStoreApi _fakeStoreApi;

    public OrderProcessor(IOptions<FakeStoreApiOptions> options)
    {
        _fakeStoreApi = RestService.For<IFakeStoreApi>(options.Value.BaseUrl);
    }

    public async Task ProcessOrderAsync(SQSEvent.SQSMessage message, ILambdaContext context)
    {
        context.Logger.LogInformation($"Processed message {message.Body}");

        var products = await _fakeStoreApi.GetProductsAsync();
        context.Logger.LogInformation($"Retrieved {products.Count} products from Fake Store API");

        foreach (var product in products)
        {
            context.Logger.LogInformation($"Product ID: {product.Id}, Title: {product.Title}, Price: {product.Price}");
        }

        await Task.CompletedTask;
    }
}