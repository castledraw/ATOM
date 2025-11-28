namespace ERP.Shared.Domain.Entities;

public record Industry(
    Guid Id,
    string Code,
    string Name,
    string Description);
