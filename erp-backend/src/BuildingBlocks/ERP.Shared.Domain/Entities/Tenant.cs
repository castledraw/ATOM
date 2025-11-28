namespace ERP.Shared.Domain.Entities;

public record Tenant(
    Guid Id,
    string Code,
    string Name,
    string Domain,
    string DefaultLanguage,
    string Status);
