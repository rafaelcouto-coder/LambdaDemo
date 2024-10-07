using Amazon.Lambda.Annotations;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Microsoft.Extensions.DependencyInjection;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace AWSLambda1
{
    public class Function
    {
        private readonly IOrderProcessorService _orderProcessor;

        public Function() : this(HostBuilderHelper.BuildHost().Services)
        {
        }

        public Function(IServiceProvider serviceProvider)
        {
            _orderProcessor = serviceProvider.GetRequiredService<IOrderProcessorService>();
        }

        [LambdaFunction]
        public async Task Handler(SQSEvent evnt, ILambdaContext context)
        {
            foreach (var message in evnt.Records)
            {
                await _orderProcessor.ProcessOrderAsync(message, context);
            }
        }
    }
}
