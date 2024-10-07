using Amazon.Lambda.Core;
using AWSLambda1.Interface;
using Refit;
using static Amazon.Lambda.SQSEvents.SQSEvent;

namespace AWSLambda1;

public class OrderProcessor : IOrderProcessorService
{
    private readonly IFakeStoreApi _fakeStoreApi;

    public OrderProcessor()
    {
        _fakeStoreApi = RestService.For<IFakeStoreApi>("https://fakestoreapi.com");
    }

    public async Task ProcessOrderAsync(
        SQSMessage message,
        ILambdaContext context)
    {
        context.Logger.LogInformation($"Processed message {message.Body}");

        var products = await _fakeStoreApi.GetProductsAsync();
        context.Logger.LogInformation($"Retrieved {products.Count} products from Fake Store API");

        for (int i = 0; i < 5; i++)
        {
            context.Logger.LogInformation($"Product ID: {products[i].Id}, Title: {products[i].Title}, Price: {products[i].Price}");
        }

        await Task.CompletedTask;
    }
}