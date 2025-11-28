namespace AdminPortal.Api.Data;

using AdminPortal.Api.Modules.Catalogs.Models;
using AdminPortal.Api.Modules.Companies.Models;
using AdminPortal.Api.Modules.Tenants.Models;

public static class AdminPortalData
{
    public static IReadOnlyList<TenantSummary> Tenants => new List<TenantSummary>
    {
        new(Guid.Parse("11111111-1111-1111-1111-111111111111"), "ACME", "Acme Corp Suite", "acme.example.com", "es-MX", 2, "active"),
        new(Guid.Parse("22222222-2222-2222-2222-222222222222"), "GLOB", "Globex Group", "globex.example.com", "en-US", 1, "pending")
    };

    public static IReadOnlyList<CompanySummary> Companies => new List<CompanySummary>
    {
        new(Guid.Parse("aaaa1111-2222-3333-4444-555555555555"), Guid.Parse("11111111-1111-1111-1111-111111111111"), "ACME-MX", "Acme Corp MX", "MXN", "es-MX", true),
        new(Guid.Parse("bbbb1111-2222-3333-4444-555555555555"), Guid.Parse("11111111-1111-1111-1111-111111111111"), "ACME-US", "Acme Corp USA", "USD", "en-US", true),
        new(Guid.Parse("cccc1111-2222-3333-4444-555555555555"), Guid.Parse("22222222-2222-2222-2222-222222222222"), "GLOB-AR", "Globex Argentina", "ARS", "es-AR", false)
    };

    public static IReadOnlyList<PaymentTermDto> PaymentTerms => new List<PaymentTermDto>
    {
        new(Guid.Parse("01010101-0101-0101-0101-010101010101"), "NET30", "Pago a 30 días", 30, "Crédito estándar"),
        new(Guid.Parse("02020202-0202-0202-0202-020202020202"), "CONTADO", "Contado", 0, "Pago inmediato"),
        new(Guid.Parse("03030303-0303-0303-0303-030303030303"), "NET15", "Pago a 15 días", 15, "Crédito ágil")
    };

    public static IReadOnlyList<CurrencyDto> Currencies => new List<CurrencyDto>
    {
        new(Guid.Parse("10000000-0000-0000-0000-000000000001"), "USD", "Dólar estadounidense", "$", 2),
        new(Guid.Parse("10000000-0000-0000-0000-000000000002"), "MXN", "Peso mexicano", "$", 2),
        new(Guid.Parse("10000000-0000-0000-0000-000000000003"), "ARS", "Peso argentino", "$", 2)
    };

    public static IReadOnlyList<LanguageDto> Languages => new List<LanguageDto>
    {
        new("es-MX", "Español (México)", true),
        new("en-US", "English (US)", false),
        new("es-AR", "Español (Argentina)", false)
    };

    public static IReadOnlyList<CustomerSummary> Customers => new List<CustomerSummary>
    {
        new(Guid.Parse("99990000-0000-0000-0000-000000000001"), Guid.Parse("aaaa1111-2222-3333-4444-555555555555"), "C-1001", "Hotel Primavera", "MXN", "NET30", 50000m, false, "Enterprise"),
        new(Guid.Parse("99990000-0000-0000-0000-000000000002"), Guid.Parse("bbbb1111-2222-3333-4444-555555555555"), "C-2001", "Globex Retail", "USD", "NET15", 25000m, false, "Premium"),
        new(Guid.Parse("99990000-0000-0000-0000-000000000003"), Guid.Parse("cccc1111-2222-3333-4444-555555555555"), "C-3001", "Distribuciones Andinas", "ARS", "CONTADO", 8000m, true, "PyME")
    };
}
