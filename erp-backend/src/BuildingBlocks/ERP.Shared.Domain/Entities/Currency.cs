namespace ERP.Shared.Domain.Entities;

public record Currency(
    Guid Id,
    string Code,
    string Name,
    string Symbol,
    int DecimalPlaces);
