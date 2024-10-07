using Microsoft.Extensions.Hosting;

namespace AWSLambda1;

public static class HostBuilderHelper
{
    public static IHost BuildHost()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                var configureDI = new DependencyInjection();
                configureDI.ConfigureServices(services);
            })
            .Build();
    }
}
