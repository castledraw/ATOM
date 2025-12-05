namespace ERP.Shared.Domain.Entities;

public record Company(
    Guid Id,
    Guid TenantId,
    string Code,
    string LegalName,
    string DefaultCurrencyCode,
    string DefaultLanguage,
    bool IsEnabled);
