using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Microsoft.Extensions.DependencyInjection;


[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambda1;

public class Function
{
    private readonly OrderProcessor _orderProcessor;

    public Function()
        : this(Startup.ConfigureServices().GetRequiredService<OrderProcessor>())
    {
    }

    public Function(OrderProcessor orderProcessor)
    {
        _orderProcessor = orderProcessor;
    }

    public async Task Handler(SQSEvent evnt, ILambdaContext context)
    {
        foreach (var message in evnt.Records)
        {
            await _orderProcessor.ProcessOrderAsync(message, context);
        }
    }
}