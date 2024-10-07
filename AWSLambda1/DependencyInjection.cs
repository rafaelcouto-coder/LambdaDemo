using Amazon.Lambda.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace AWSLambda1
{
    [LambdaStartup]
    public class DependencyInjection
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IOrderProcessorService, OrderProcessor>();
        }
    }
}
