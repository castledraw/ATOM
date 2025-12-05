namespace AdminPortal.Api.Modules.Catalogs.Models;

public record PaymentTermDto(Guid Id, string Code, string Name, int Days, string Description);
