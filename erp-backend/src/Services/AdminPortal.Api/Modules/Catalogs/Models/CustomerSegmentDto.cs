namespace AdminPortal.Api.Modules.Catalogs.Models;

public record CustomerSegmentDto(Guid Id, Guid CompanyId, string Code, string Name, string Description, bool IsDefault);
