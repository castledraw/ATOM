using AdminPortal.Api.Modules.Tenants.Models;
using ERP.Shared.Application.CQRS;
using ERP.Shared.Domain.Abstractions;
using ERP.Shared.Domain.Entities;

namespace AdminPortal.Api.Modules.Tenants.Queries;

public sealed record GetTenantDetailQuery(Guid TenantId) : IQuery<TenantSummary?>;

public sealed class GetTenantDetailHandler : IQueryHandler<GetTenantDetailQuery, TenantSummary?>
{
    private readonly IReadRepository<Tenant> _tenantRepository;
    private readonly IReadRepository<Company> _companyRepository;

    public GetTenantDetailHandler(IReadRepository<Tenant> tenantRepository, IReadRepository<Company> companyRepository)
    {
        _tenantRepository = tenantRepository;
        _companyRepository = companyRepository;
    }

    public async Task<TenantSummary?> Handle(GetTenantDetailQuery query, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.SingleOrDefaultAsync(t => t.Id == query.TenantId, cancellationToken);
        if (tenant is null)
        {
            return null;
        }

        var companyCount = (await _companyRepository.ListAsync(cancellationToken)).Count(c => c.TenantId == tenant.Id);
        return new TenantSummary(tenant.Id, tenant.Code, tenant.Name, tenant.Domain, tenant.DefaultLanguage, companyCount, tenant.Status);
    }
}
