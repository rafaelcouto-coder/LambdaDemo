using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using AWSLambda1.Service;
using Microsoft.Extensions.Logging;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace AWSLambda1;

public class Function
{
    private readonly ILogger<Function> _logger;
    private readonly IOrderProcessorService _orderProcessor;

    public Function(
        ILogger<Function> logger,
        IOrderProcessorService orderProcessorService)
    {
        _logger = logger;
        _orderProcessor = orderProcessorService;
    }

    public async Task Handler(SQSEvent evnt, ILambdaContext context)
    {
        _logger.LogInformation("Lambda invoked");

        foreach (var message in evnt.Records)
        {
            await _orderProcessor.ProcessOrderAsync(message);
        }
    }
}
