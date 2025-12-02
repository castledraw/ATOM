using AdminPortal.Api.Modules.Companies.Models;
using ERP.Shared.Application.CQRS;
using ERP.Shared.Domain.Abstractions;
using ERP.Shared.Domain.Entities;

namespace AdminPortal.Api.Modules.Companies.Commands;

public sealed record CreateCompanyCommand(Guid TenantId, string Code, string LegalName, string DefaultCurrency, string DefaultLanguage) : ICommand<CompanySummary>;

public sealed record UpdateCompanyCommand(Guid Id, string LegalName, string DefaultCurrency, string DefaultLanguage, bool Enabled) : ICommand<CompanySummary?>;

public sealed record RegisterBranchCommand(Guid CompanyId, string Code, string Name, string? PhoneNumber, string? Email, string? AddressLabel, bool Enabled) : ICommand<CompanyBranchSummary>;

public sealed record ToggleCompanyStatusCommand(Guid CompanyId, bool Enabled) : ICommand<CompanySummary?>;

public class CompanyCommandHandler :
    ICommandHandler<CreateCompanyCommand, CompanySummary>,
    ICommandHandler<UpdateCompanyCommand, CompanySummary?>,
    ICommandHandler<RegisterBranchCommand, CompanyBranchSummary>,
    ICommandHandler<ToggleCompanyStatusCommand, CompanySummary?>
{
    private readonly IRepository<Company> _companies;
    private readonly IRepository<CompanyBranch> _branches;

    public CompanyCommandHandler(IRepository<Company> companies, IRepository<CompanyBranch> branches)
    {
        _companies = companies;
        _branches = branches;
    }

    public async Task<CompanySummary> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        var company = new Company(Guid.NewGuid(), command.TenantId, command.Code, command.LegalName, command.DefaultCurrency, command.DefaultLanguage, true);
        await _companies.AddAsync(company, cancellationToken);
        return ToSummary(company);
    }

    public async Task<CompanySummary?> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        var updated = await _companies.UpdateAsync(c => c.Id == command.Id, existing => existing with
        {
            LegalName = command.LegalName,
            DefaultCurrencyCode = command.DefaultCurrency,
            DefaultLanguage = command.DefaultLanguage,
            IsEnabled = command.Enabled
        }, cancellationToken);

        return updated is null ? null : ToSummary(updated);
    }

    public async Task<CompanyBranchSummary> Handle(RegisterBranchCommand command, CancellationToken cancellationToken)
    {
        var branch = new CompanyBranch(Guid.NewGuid(), command.CompanyId, command.Code, command.Name, command.PhoneNumber, command.Email, command.AddressLabel, command.Enabled);
        await _branches.AddAsync(branch, cancellationToken);
        return new CompanyBranchSummary(branch.Id, branch.CompanyId, branch.Code, branch.Name, branch.PhoneNumber, branch.Email, branch.AddressLabel, branch.Enabled);
    }

    public async Task<CompanySummary?> Handle(ToggleCompanyStatusCommand command, CancellationToken cancellationToken)
    {
        var updated = await _companies.UpdateAsync(c => c.Id == command.CompanyId, existing => existing with { IsEnabled = command.Enabled }, cancellationToken);
        return updated is null ? null : ToSummary(updated);
    }

    private static CompanySummary ToSummary(Company company) => new(company.Id, company.TenantId, company.Code, company.LegalName, company.DefaultCurrencyCode, company.DefaultLanguage, company.IsEnabled);
}
