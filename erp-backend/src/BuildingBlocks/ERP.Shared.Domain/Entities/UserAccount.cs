namespace ERP.Shared.Domain.Entities;

public record UserAccount(
    Guid Id,
    string Email,
    string TenantCode,
    string[] Roles,
    bool Enabled);
