using AdminPortal.Api.Modules.Companies.Models;
using ERP.Shared.Application.CQRS;
using ERP.Shared.Domain.Abstractions;
using ERP.Shared.Domain.Entities;

namespace AdminPortal.Api.Modules.Companies.Queries;

public sealed record GetCompanyBranchesQuery(Guid CompanyId) : IQuery<IReadOnlyList<CompanyBranchSummary>>;

public sealed class GetCompanyBranchesHandler : IQueryHandler<GetCompanyBranchesQuery, IReadOnlyList<CompanyBranchSummary>>
{
    private readonly IReadRepository<CompanyBranch> _branchRepository;

    public GetCompanyBranchesHandler(IReadRepository<CompanyBranch> branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<IReadOnlyList<CompanyBranchSummary>> Handle(GetCompanyBranchesQuery query, CancellationToken cancellationToken)
    {
        var branches = await _branchRepository.ListAsync(b => b.CompanyId == query.CompanyId, cancellationToken);
        return branches
            .Select(b => new CompanyBranchSummary(b.Id, b.CompanyId, b.Code, b.Name, b.PhoneNumber, b.Email, b.Address, b.IsEnabled))
            .ToList();
    }
}
