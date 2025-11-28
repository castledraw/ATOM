using AdminPortal.Api.Modules.Tenants.Models;

namespace AdminPortal.Api.Modules.Tenants.Queries;

public static class GetTenants
{
    public static IEnumerable<TenantSummary> Handle()
    {
        // TODO: reemplazar por query CQRS con filtros y paginación sobre PostgreSQL
        return new List<TenantSummary>
        {
            new(Guid.NewGuid(), "Tenant Alpha", "active"),
            new(Guid.NewGuid(), "Tenant Beta", "provisioning")
        };
    }
}
