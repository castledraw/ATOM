using AdminPortal.Api.Modules.Companies.Models;
using ERP.Shared.Application.CQRS;
using ERP.Shared.Domain.Abstractions;
using ERP.Shared.Domain.Entities;

namespace AdminPortal.Api.Modules.Companies.Queries;

public sealed record GetCompanyCustomersQuery(Guid CompanyId) : IQuery<IReadOnlyList<CustomerSummary>>;

public sealed class GetCompanyCustomersHandler : IQueryHandler<GetCompanyCustomersQuery, IReadOnlyList<CustomerSummary>>
{
    private readonly IReadRepository<Customer> _customerRepository;

    public GetCompanyCustomersHandler(IReadRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IReadOnlyList<CustomerSummary>> Handle(GetCompanyCustomersQuery query, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.ListAsync(c => c.CompanyId == query.CompanyId, cancellationToken);
        return customers
            .Select(c => new CustomerSummary(c.Id, c.CompanyId, c.CustomerNumber, c.Name, c.CurrencyCode, c.PaymentTermCode, c.CreditLimit, c.CreditBlocked, c.Segment, c.BranchCode))
            .ToList();
    }
}
