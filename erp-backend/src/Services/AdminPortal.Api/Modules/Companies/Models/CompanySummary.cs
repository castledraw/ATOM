namespace AdminPortal.Api.Modules.Companies.Models;

public record CompanySummary(
    Guid Id,
    Guid TenantId,
    string Code,
    string LegalName,
    string DefaultCurrency,
    string DefaultLanguage,
    bool Enabled);
