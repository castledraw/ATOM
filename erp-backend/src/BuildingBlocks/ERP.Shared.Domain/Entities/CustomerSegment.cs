namespace ERP.Shared.Domain.Entities;

public record CustomerSegment(
    Guid Id,
    Guid CompanyId,
    string Code,
    string Name,
    string Description,
    bool IsDefault);
