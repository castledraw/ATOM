namespace AdminPortal.Api.Modules.Tenants.Models;

public record TenantSummary(
    Guid Id,
    string Code,
    string Name,
    string Domain,
    string DefaultLanguage,
    int Companies,
    string Status);
