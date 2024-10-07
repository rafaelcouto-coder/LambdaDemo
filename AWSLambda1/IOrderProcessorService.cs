using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;

namespace AWSLambda1;

public interface IOrderProcessorService
{
    Task ProcessOrderAsync(SQSEvent.SQSMessage message, ILambdaContext context);
}