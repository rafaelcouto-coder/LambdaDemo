using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Annotations;
using AWSLambda1.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AWSLambda1;

[LambdaStartup]
public class Startup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAWSService<IAmazonDynamoDB>();
        services.AddScoped<IDynamoDBContext, DynamoDBContext>();

        services.Configure<FakeStoreApiOptions>(options =>
            configuration.GetSection("FakeStoreApi").Bind(options));

        services.AddTransient<ProcessOrdenUseCase>();
    }
}
