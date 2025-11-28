namespace AdminPortal.Api.Modules.Companies.Models;

public record CompanyBranchSummary(
    Guid Id,
    Guid CompanyId,
    string Code,
    string Name,
    string? PhoneNumber,
    string? Email,
    string? AddressLabel,
    bool Enabled);
