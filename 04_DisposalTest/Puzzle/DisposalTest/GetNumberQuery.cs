using Microsoft.Extensions.DependencyInjection;

namespace DisposalTest;

sealed class GetNumberQuery
{
    readonly IServiceScopeFactory _scopeFactory;

    public GetNumberQuery(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public Task<int> ExecuteQueryAsync()
    {
        IServiceScope scope = _scopeFactory.CreateScope();
        INumberService numberService = scope.ServiceProvider.GetRequiredService<INumberService>();

        return numberService.GetNumberAsync();
    }
}
