using ERP.Shared.Application.CQRS;
using ERP.Shared.Domain.Abstractions;
using ERP.Shared.Domain.Entities;

namespace ErpTenant.Api.Customers.Queries;

public sealed record GetCustomersQuery(string? TenantCode, string? CompanyCode, string? BranchCode) : IQuery<IReadOnlyList<CustomerDetail>>;

public sealed class GetCustomersHandler : IQueryHandler<GetCustomersQuery, IReadOnlyList<CustomerDetail>>
{
    private readonly IReadRepository<Customer> _customerRepository;
    private readonly IReadRepository<Company> _companyRepository;
    private readonly IReadRepository<Tenant> _tenantRepository;

    public GetCustomersHandler(IReadRepository<Customer> customerRepository, IReadRepository<Company> companyRepository, IReadRepository<Tenant> tenantRepository)
    {
        _customerRepository = customerRepository;
        _companyRepository = companyRepository;
        _tenantRepository = tenantRepository;
    }

    public async Task<IReadOnlyList<CustomerDetail>> Handle(GetCustomersQuery query, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.ListAsync(cancellationToken);
        var companies = await _companyRepository.ListAsync(cancellationToken);
        var tenants = await _tenantRepository.ListAsync(cancellationToken);

        var projection = customers
            .Select(customer =>
            {
                var company = companies.First(c => c.Id == customer.CompanyId);
                var tenant = tenants.First(t => t.Id == company.TenantId);
                return new CustomerDetail(
                    customer.Id,
                    customer.CustomerNumber,
                    customer.Name,
                    customer.CurrencyCode,
                    customer.PaymentTermCode,
                    customer.CreditLimit,
                    customer.CreditBlocked,
                    customer.Segment,
                    company.Code,
                    tenant.Code,
                    customer.BranchCode,
                    customer.LanguageCode);
            });

        if (!string.IsNullOrWhiteSpace(query.TenantCode))
        {
            projection = projection.Where(c => string.Equals(c.TenantCode, query.TenantCode, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.CompanyCode))
        {
            projection = projection.Where(c => string.Equals(c.CompanyCode, query.CompanyCode, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.BranchCode))
        {
            projection = projection.Where(c => string.Equals(c.BranchCode, query.BranchCode, StringComparison.OrdinalIgnoreCase));
        }

        return projection.ToList();
    }
}
