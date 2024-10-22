using AWSLambda1.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AWSLambda1;

public class LambdaEntryPoint
{
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddLogging();
                services.AddTransient<IOrderProcessorService, OrderProcessor>();
            });
}