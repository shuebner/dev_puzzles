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
    public void ExecuteQuery_returns_result_from_service()
    {
    }

    [Fact]
    public void ExecuteQuery_when_number_service_is_disposable_Then_disposes_it_after_it_has_completed()
    {
    }
}
