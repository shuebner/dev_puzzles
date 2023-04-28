using Microsoft.Extensions.DependencyInjection;

namespace DisposalTest;

sealed class GetNumberQuery
{
    readonly IServiceScopeFactory _scopeFactory;

    public GetNumberQuery(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task<int> ExecuteQueryAsync()
    {
        using IServiceScope scope = _scopeFactory.CreateScope();
        INumberService numberService = scope.ServiceProvider.GetRequiredService<INumberService>();

        return await numberService.GetNumberAsync();
    }
}
