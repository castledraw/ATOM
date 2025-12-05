using AdminPortal.Api.Modules.Tenants.Models;
using ERP.Shared.Application.CQRS;
using ERP.Shared.Domain.Abstractions;
using ERP.Shared.Domain.Entities;

namespace AdminPortal.Api.Modules.Tenants.Commands;

public sealed record CreateTenantCommand(string Code, string Name, string Domain, string DefaultLanguage) : ICommand<TenantSummary>;

public sealed record UpdateTenantCommand(Guid Id, string Name, string Domain, string DefaultLanguage, string Status) : ICommand<TenantSummary?>;

public sealed record SetTenantStatusCommand(Guid Id, string Status) : ICommand<TenantSummary?>;

public class TenantCommandHandler :
    ICommandHandler<CreateTenantCommand, TenantSummary>,
    ICommandHandler<UpdateTenantCommand, TenantSummary?>,
    ICommandHandler<SetTenantStatusCommand, TenantSummary?>
{
    private readonly IRepository<Tenant> _tenantRepository;
    private readonly IReadRepository<Company> _companyRepository;

    public TenantCommandHandler(IRepository<Tenant> tenantRepository, IReadRepository<Company> companyRepository)
    {
        _tenantRepository = tenantRepository;
        _companyRepository = companyRepository;
    }

    public async Task<TenantSummary> Handle(CreateTenantCommand command, CancellationToken cancellationToken)
    {
        var entity = new Tenant(Guid.NewGuid(), command.Code, command.Name, command.Domain, command.DefaultLanguage, "active");
        await _tenantRepository.AddAsync(entity, cancellationToken);
        return await BuildSummary(entity, cancellationToken);
    }

    public async Task<TenantSummary?> Handle(UpdateTenantCommand command, CancellationToken cancellationToken)
    {
        var updated = await _tenantRepository.UpdateAsync(t => t.Id == command.Id, existing => existing with
        {
            Name = command.Name,
            Domain = command.Domain,
            DefaultLanguage = command.DefaultLanguage,
            Status = command.Status
        }, cancellationToken);

        return updated is null ? null : await BuildSummary(updated, cancellationToken);
    }

    public async Task<TenantSummary?> Handle(SetTenantStatusCommand command, CancellationToken cancellationToken)
    {
        var updated = await _tenantRepository.UpdateAsync(t => t.Id == command.Id, existing => existing with { Status = command.Status }, cancellationToken);
        return updated is null ? null : await BuildSummary(updated, cancellationToken);
    }

    private async Task<TenantSummary> BuildSummary(Tenant tenant, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.ListAsync(c => c.TenantId == tenant.Id, cancellationToken);
        return new TenantSummary(
            tenant.Id,
            tenant.Code,
            tenant.Name,
            tenant.Domain,
            tenant.DefaultLanguage,
            companies.Count,
            tenant.Status);
    }
}
