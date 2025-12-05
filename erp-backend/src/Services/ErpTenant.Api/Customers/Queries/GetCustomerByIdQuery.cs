using ERP.Shared.Application.CQRS;
using ERP.Shared.Domain.Abstractions;
using ERP.Shared.Domain.Entities;

namespace ErpTenant.Api.Customers.Queries;

public sealed record GetCustomerByIdQuery(Guid Id) : IQuery<CustomerDetail?>;

public sealed class GetCustomerByIdHandler : IQueryHandler<GetCustomerByIdQuery, CustomerDetail?>
{
    private readonly IReadRepository<Customer> _customerRepository;
    private readonly IReadRepository<Company> _companyRepository;
    private readonly IReadRepository<Tenant> _tenantRepository;

    public GetCustomerByIdHandler(IReadRepository<Customer> customerRepository, IReadRepository<Company> companyRepository, IReadRepository<Tenant> tenantRepository)
    {
        _customerRepository = customerRepository;
        _companyRepository = companyRepository;
        _tenantRepository = tenantRepository;
    }

    public async Task<CustomerDetail?> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.SingleOrDefaultAsync(c => c.Id == query.Id, cancellationToken);
        if (customer is null)
        {
            return null;
        }

        var company = (await _companyRepository.SingleOrDefaultAsync(c => c.Id == customer.CompanyId, cancellationToken))!;
        var tenant = (await _tenantRepository.SingleOrDefaultAsync(t => t.Id == company.TenantId, cancellationToken))!;

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
    }
}
