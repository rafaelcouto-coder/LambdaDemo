using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.SQSEvents;
using Amazon.Lambda.TestUtilities;
using AWSLambda1.Options;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace AWSLambda1.Tests;

public class FunctionTest
{
    private readonly Mock<IDynamoDBContext> _mockContext;
    private readonly Mock<OrderProcessor> _mockProcessOrdenUseCase;
    private readonly Mock<IOptions<FakeStoreApiOptions>> _mockOptions;

    public FunctionTest()
    {
        _mockContext = new Mock<IDynamoDBContext>();
        _mockOptions = new Mock<IOptions<FakeStoreApiOptions>>();

        _mockOptions.Setup(o => o.Value).Returns(new FakeStoreApiOptions
        {
            BaseUrl = "https://fakestoreapi.com"
        });

        _mockProcessOrdenUseCase = new Mock<OrderProcessor>(_mockOptions.Object);
    }

    [Fact]
    public async Task TestSQSEventLambdaFunction()
    {
        var sqsEvent = new SQSEvent
        {
            Records = new List<SQSEvent.SQSMessage>
                {
                    new SQSEvent.SQSMessage
                    {
                        Body = "foobar"
                    }
                }
        };

        var logger = new TestLambdaLogger();
        var context = new TestLambdaContext
        {
            Logger = logger
        };

        var function = new Function(_mockContext.Object, _mockProcessOrdenUseCase.Object);
        await function.Handler(sqsEvent, context);

        Assert.Contains("Processed message foobar", logger.Buffer.ToString());
    }
}
