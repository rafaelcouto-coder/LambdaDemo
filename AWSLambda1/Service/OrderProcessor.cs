using AWSLambda1.Interface;
using Microsoft.Extensions.Logging;
using Refit;
using static Amazon.Lambda.SQSEvents.SQSEvent;

namespace AWSLambda1.Service;

public class OrderProcessor : IOrderProcessorService
{
    private readonly IFakeStoreApi _fakeStoreApi;
    private readonly ILogger<OrderProcessor> _logger;

    public OrderProcessor(ILogger<OrderProcessor> logger)
    {
        _fakeStoreApi = RestService.For<IFakeStoreApi>("https://fakestoreapi.com");
        _logger = logger;
    }

    public async Task ProcessOrderAsync(
        SQSMessage message)
    {
        _logger.LogInformation($"Processed message {message.Body}");

        var products = await _fakeStoreApi.GetProductsAsync();
        _logger.LogInformation($"Retrieved {products.Count} products from Fake Store API");

        for (int i = 0; i < 5; i++)
        {
            _logger.LogInformation($"Product ID: {products[i].Id}, Title: {products[i].Title}, Price: {products[i].Price}");
        }

        await Task.CompletedTask;
    }
}