using ERP.Shared.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ERP.Shared.Infrastructure.InMemory;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedInMemoryRepositories(this IServiceCollection services)
    {
        services.AddSingleton(_ => ErpSeedData.CreateStore());
        services.AddSingleton(typeof(IRepository<>), typeof(InMemoryRepository<>));
        services.AddSingleton(typeof(IReadRepository<>), sp => sp.GetRequiredService(typeof(IRepository<>)));
        services.AddSingleton(typeof(IWriteRepository<>), sp => sp.GetRequiredService(typeof(IRepository<>)));
        return services;
    }
}
