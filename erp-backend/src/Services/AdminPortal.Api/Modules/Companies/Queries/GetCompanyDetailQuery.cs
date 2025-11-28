using AdminPortal.Api.Modules.Companies.Models;
using ERP.Shared.Application.CQRS;
using ERP.Shared.Domain.Abstractions;
using ERP.Shared.Domain.Entities;

namespace AdminPortal.Api.Modules.Companies.Queries;

public sealed record GetCompanyDetailQuery(Guid CompanyId) : IQuery<CompanySummary?>;

public sealed class GetCompanyDetailHandler : IQueryHandler<GetCompanyDetailQuery, CompanySummary?>
{
    private readonly IReadRepository<Company> _companyRepository;

    public GetCompanyDetailHandler(IReadRepository<Company> companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<CompanySummary?> Handle(GetCompanyDetailQuery query, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.SingleOrDefaultAsync(c => c.Id == query.CompanyId, cancellationToken);
        return company is null
            ? null
            : new CompanySummary(company.Id, company.TenantId, company.Code, company.LegalName, company.DefaultCurrencyCode, company.DefaultLanguage, company.IsEnabled);
    }
}
