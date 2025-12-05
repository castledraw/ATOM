namespace AdminPortal.Api.Modules.Companies.Models;

public record CustomerSummary(
    Guid Id,
    Guid CompanyId,
    string CustomerNumber,
    string DisplayName,
    string Currency,
    string PaymentTerm,
    decimal CreditLimit,
    bool CreditBlocked,
    string Segment,
    string? BranchCode);
