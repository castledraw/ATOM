using AdminPortal.Api.Modules.Tenants.Models;
using ERP.Shared.Application.CQRS;
using ERP.Shared.Domain.Abstractions;
using ERP.Shared.Domain.Entities;

namespace AdminPortal.Api.Modules.Tenants.Queries;

public sealed record GetTenantsQuery : IQuery<IReadOnlyList<TenantSummary>>;

public sealed class GetTenantsHandler : IQueryHandler<GetTenantsQuery, IReadOnlyList<TenantSummary>>
{
    private readonly IReadRepository<Tenant> _tenantRepository;
    private readonly IReadRepository<Company> _companyRepository;

    public GetTenantsHandler(IReadRepository<Tenant> tenantRepository, IReadRepository<Company> companyRepository)
    {
        _tenantRepository = tenantRepository;
        _companyRepository = companyRepository;
    }

    public async Task<IReadOnlyList<TenantSummary>> Handle(GetTenantsQuery query, CancellationToken cancellationToken)
    {
        var tenants = await _tenantRepository.ListAsync(cancellationToken);
        var companies = await _companyRepository.ListAsync(cancellationToken);

        var result = tenants
            .Select(t => new TenantSummary(
                t.Id,
                t.Code,
                t.Name,
                t.Domain,
                t.DefaultLanguage,
                companies.Count(c => c.TenantId == t.Id),
                t.Status))
            .ToList();

        return result;
    }
}
