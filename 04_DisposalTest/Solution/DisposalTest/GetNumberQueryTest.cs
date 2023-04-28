using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace DisposalTest;

public class GetNumberQueryTest
{
    readonly Mock<INumberService> _numberServiceMock;

    readonly GetNumberQuery _query;

    public GetNumberQueryTest()
    {
        ServiceCollection services = new();

        _numberServiceMock = new Mock<INumberService>();
        services.AddScoped(_ => _numberServiceMock.Object);

        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IServiceScopeFactory serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

        _query = new GetNumberQuery(serviceScopeFactory);
    }

    [Fact]
    public async Task ExecuteQuery_returns_result_from_service()
    {
        _numberServiceMock.Setup(s => s.GetNumberAsync()).ReturnsAsync(42);

        int result = await _query.ExecuteQueryAsync();

        Assert.Equal(expected: 42, actual: result);
    }

    [Fact]
    public void ExecuteQuery_when_number_service_is_disposable_Then_disposes_it_after_it_has_completed()
    {
        var numberServiceDisposable = _numberServiceMock.As<IDisposable>();
        TaskCompletionSource<int> tcs = new();
        _numberServiceMock.Setup(s => s.GetNumberAsync()).Returns(tcs.Task);

        Task executeTask = _query.ExecuteQueryAsync();

        Assert.False(executeTask.IsCompleted);
        numberServiceDisposable.Verify(s => s.Dispose(), Times.Never);
        tcs.SetResult(42);
        Assert.True(executeTask.IsCompletedSuccessfully);
        numberServiceDisposable.Verify(s => s.Dispose(), Times.Once);
    }
}
