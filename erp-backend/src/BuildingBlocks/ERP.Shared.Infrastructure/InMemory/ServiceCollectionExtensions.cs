using ERP.Shared.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ERP.Shared.Infrastructure.InMemory;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedInMemoryReadModels(this IServiceCollection services)
    {
        services.AddSingleton(_ => ErpSeedData.CreateStore());
        services.AddSingleton(typeof(IReadRepository<>), typeof(InMemoryReadRepository<>));
        return services;
    }
}
