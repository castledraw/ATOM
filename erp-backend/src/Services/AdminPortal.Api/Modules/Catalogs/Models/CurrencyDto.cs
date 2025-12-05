namespace AdminPortal.Api.Modules.Catalogs.Models;

public record CurrencyDto(Guid Id, string Code, string Name, string Symbol, int DecimalPlaces);
