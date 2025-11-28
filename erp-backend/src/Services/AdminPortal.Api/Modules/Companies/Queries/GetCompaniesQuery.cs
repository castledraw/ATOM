using AdminPortal.Api.Modules.Companies.Models;
using ERP.Shared.Application.CQRS;
using ERP.Shared.Domain.Abstractions;
using ERP.Shared.Domain.Entities;

namespace AdminPortal.Api.Modules.Companies.Queries;

public sealed record GetCompaniesQuery : IQuery<IReadOnlyList<CompanySummary>>;

public sealed class GetCompaniesHandler : IQueryHandler<GetCompaniesQuery, IReadOnlyList<CompanySummary>>
{
    private readonly IReadRepository<Company> _companyRepository;

    public GetCompaniesHandler(IReadRepository<Company> companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<IReadOnlyList<CompanySummary>> Handle(GetCompaniesQuery query, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.ListAsync(cancellationToken);
        return companies
            .Select(c => new CompanySummary(c.Id, c.TenantId, c.Code, c.LegalName, c.DefaultCurrencyCode, c.DefaultLanguage, c.IsEnabled))
            .ToList();
    }
}
