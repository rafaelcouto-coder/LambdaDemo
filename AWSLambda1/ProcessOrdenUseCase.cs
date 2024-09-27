using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using AWSLambda1.Interface;
using AWSLambda1.Options;
using Microsoft.Extensions.Options;
using Refit;

namespace AWSLambda1;

public class ProcessOrdenUseCase
{
    private readonly IFakeStoreApi _fakeStoreApi;

    public ProcessOrdenUseCase(IOptions<FakeStoreApiOptions> options)
    {
        _fakeStoreApi = RestService.For<IFakeStoreApi>(options.Value.BaseUrl);
    }

    public async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
    {
        context.Logger.LogInformation($"Processed message {message.Body}");

        // Chamada para a API Fake Store
        var products = await _fakeStoreApi.GetProductsAsync();
        context.Logger.LogInformation($"Retrieved {products.Count} products from Fake Store API");

        // TODO: Do interesting work based on the new message
        foreach (var product in products)
        {
            context.Logger.LogInformation($"Product ID: {product.Id}, Title: {product.Title}, Price: {product.Price}");
        }

        await Task.CompletedTask;
    }
}