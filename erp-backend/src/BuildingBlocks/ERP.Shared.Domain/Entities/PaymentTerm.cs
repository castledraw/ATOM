namespace ERP.Shared.Domain.Entities;

public record PaymentTerm(
    Guid Id,
    string Code,
    string Name,
    int Days,
    string Description);
