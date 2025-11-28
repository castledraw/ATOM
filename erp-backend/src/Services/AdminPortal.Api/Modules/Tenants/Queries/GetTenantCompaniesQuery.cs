using AdminPortal.Api.Modules.Companies.Models;
using ERP.Shared.Application.CQRS;
using ERP.Shared.Domain.Abstractions;
using ERP.Shared.Domain.Entities;

namespace AdminPortal.Api.Modules.Tenants.Queries;

public sealed record GetTenantCompaniesQuery(Guid TenantId) : IQuery<IReadOnlyList<CompanySummary>>;

public sealed class GetTenantCompaniesHandler : IQueryHandler<GetTenantCompaniesQuery, IReadOnlyList<CompanySummary>>
{
    private readonly IReadRepository<Company> _companyRepository;

    public GetTenantCompaniesHandler(IReadRepository<Company> companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<IReadOnlyList<CompanySummary>> Handle(GetTenantCompaniesQuery query, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.ListAsync(c => c.TenantId == query.TenantId, cancellationToken);
        return companies
            .Select(c => new CompanySummary(c.Id, c.TenantId, c.Code, c.LegalName, c.DefaultCurrencyCode, c.DefaultLanguage, c.IsEnabled))
            .ToList();
    }
}
