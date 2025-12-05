namespace ERP.Shared.Domain.Entities;

public record Customer(
    Guid Id,
    Guid CompanyId,
    string CustomerNumber,
    string Name,
    string CurrencyCode,
    string PaymentTermCode,
    decimal CreditLimit,
    bool CreditBlocked,
    string Segment,
    string BranchCode,
    string LanguageCode);
