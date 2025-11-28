namespace ERP.Shared.Domain.Entities;

public record Language(
    string Code,
    string Name,
    bool IsDefault);
