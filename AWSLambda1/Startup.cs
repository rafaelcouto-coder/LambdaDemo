using Amazon.Lambda.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace AWSLambda1;

[LambdaStartup]
public class Startup
{
    public static ServiceProvider ConfigureServices()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddTransient<OrderProcessor>();

        return serviceCollection.BuildServiceProvider();
    }
}