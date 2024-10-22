using Amazon.Lambda.SQSEvents;

namespace AWSLambda1.Service;

public interface IOrderProcessorService
{
    Task ProcessOrderAsync(SQSEvent.SQSMessage message);
}