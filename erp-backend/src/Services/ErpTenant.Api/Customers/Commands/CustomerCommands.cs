using ERP.Shared.Application.CQRS;
using ERP.Shared.Domain.Abstractions;
using ERP.Shared.Domain.Entities;
using ErpTenant.Api.Customers;

namespace ErpTenant.Api.Customers.Commands;

public sealed record CreateCustomerCommand(Guid CompanyId, string CustomerNumber, string DisplayName, string Currency, string PaymentTerm, decimal CreditLimit, bool CreditBlocked, string Segment, string? BranchCode, string? LanguageCode) : ICommand<CustomerDetail>;

public sealed record UpdateCustomerCreditCommand(Guid Id, decimal CreditLimit, bool CreditBlocked) : ICommand<CustomerDetail?>;

public sealed record UpdateCustomerProfileCommand(Guid Id, string DisplayName, string Segment, string? BranchCode, string? LanguageCode) : ICommand<CustomerDetail?>;

public class CustomerCommandHandler :
    ICommandHandler<CreateCustomerCommand, CustomerDetail>,
    ICommandHandler<UpdateCustomerCreditCommand, CustomerDetail?>,
    ICommandHandler<UpdateCustomerProfileCommand, CustomerDetail?>
{
    private readonly IRepository<Customer> _customers;
    private readonly IReadRepository<Company> _companies;
    private readonly IReadRepository<Tenant> _tenants;

    public CustomerCommandHandler(IRepository<Customer> customers, IReadRepository<Company> companies, IReadRepository<Tenant> tenants)
    {
        _customers = customers;
        _companies = companies;
        _tenants = tenants;
    }

    public async Task<CustomerDetail> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = new Customer(Guid.NewGuid(), command.CompanyId, command.CustomerNumber, command.DisplayName, command.Currency, command.PaymentTerm, command.CreditLimit, command.CreditBlocked, command.Segment, command.BranchCode, command.LanguageCode);
        await _customers.AddAsync(customer, cancellationToken);
        return await ToDetail(customer, cancellationToken);
    }

    public async Task<CustomerDetail?> Handle(UpdateCustomerCreditCommand command, CancellationToken cancellationToken)
    {
        var updated = await _customers.UpdateAsync(c => c.Id == command.Id, existing => existing with
        {
            CreditLimit = command.CreditLimit,
            CreditBlocked = command.CreditBlocked
        }, cancellationToken);

        return updated is null ? null : await ToDetail(updated, cancellationToken);
    }

    public async Task<CustomerDetail?> Handle(UpdateCustomerProfileCommand command, CancellationToken cancellationToken)
    {
        var updated = await _customers.UpdateAsync(c => c.Id == command.Id, existing => existing with
        {
            DisplayName = command.DisplayName,
            Segment = command.Segment,
            BranchCode = command.BranchCode,
            LanguageCode = command.LanguageCode
        }, cancellationToken);

        return updated is null ? null : await ToDetail(updated, cancellationToken);
    }

    private async Task<CustomerDetail> ToDetail(Customer customer, CancellationToken cancellationToken)
    {
        var company = await _companies.SingleOrDefaultAsync(c => c.Id == customer.CompanyId, cancellationToken) ?? throw new InvalidOperationException("Company not found");
        var tenant = await _tenants.SingleOrDefaultAsync(t => t.Id == company.TenantId, cancellationToken) ?? throw new InvalidOperationException("Tenant not found");
        return new CustomerDetail(customer.Id, customer.CustomerNumber, customer.DisplayName, customer.Currency, customer.PaymentTerm, customer.CreditLimit, customer.CreditBlocked, customer.Segment, company.Code, tenant.Code, customer.BranchCode, customer.LanguageCode);
    }
}
