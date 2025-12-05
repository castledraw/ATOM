namespace ERP.Shared.Domain.Entities;

public record CompanyBranch(
    Guid Id,
    Guid CompanyId,
    string Code,
    string Name,
    string PhoneNumber,
    string Email,
    string Address,
    bool IsEnabled);
