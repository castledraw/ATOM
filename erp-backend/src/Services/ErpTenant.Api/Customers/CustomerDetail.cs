namespace ErpTenant.Api.Customers;

public record CustomerDetail(
    Guid Id,
    string CustomerNumber,
    string DisplayName,
    string Currency,
    string PaymentTerm,
    decimal CreditLimit,
    bool CreditBlocked,
    string Segment,
    string CompanyCode,
    string TenantCode);
