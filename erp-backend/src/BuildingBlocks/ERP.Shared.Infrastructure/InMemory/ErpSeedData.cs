using ERP.Shared.Domain.Entities;

namespace ERP.Shared.Infrastructure.InMemory;

public static class ErpSeedData
{
    public static InMemoryDataStore CreateStore()
    {
        var store = new InMemoryDataStore();

        var tenants = new List<Tenant>
        {
            new(Guid.Parse("11111111-1111-1111-1111-111111111111"), "ACME", "Acme Corp Suite", "acme.example.com", "es-MX", "active"),
            new(Guid.Parse("22222222-2222-2222-2222-222222222222"), "GLOB", "Globex Group", "globex.example.com", "en-US", "pending")
        };

        var companies = new List<Company>
        {
            new(Guid.Parse("aaaa1111-2222-3333-4444-555555555555"), tenants[0].Id, "ACME-MX", "Acme Corp MX", "MXN", "es-MX", true),
            new(Guid.Parse("bbbb1111-2222-3333-4444-555555555555"), tenants[0].Id, "ACME-US", "Acme Corp USA", "USD", "en-US", true),
            new(Guid.Parse("cccc1111-2222-3333-4444-555555555555"), tenants[1].Id, "GLOB-AR", "Globex Argentina", "ARS", "es-AR", false)
        };

        var branches = new List<CompanyBranch>
        {
            new(Guid.Parse("d1d1d1d1-2222-3333-4444-555555555555"), companies[0].Id, "MX-HQ", "CDMX Centro", "+52 55 1234 5678", "cdmx@acme.com", "Av. Reforma 101, CDMX", true),
            new(Guid.Parse("d2d2d2d2-2222-3333-4444-555555555555"), companies[0].Id, "MTY-SAT", "Monterrey Satélite", "+52 81 1111 2222", "mty@acme.com", "Blvd. Sendero 55, MTY", true),
            new(Guid.Parse("d3d3d3d3-2222-3333-4444-555555555555"), companies[1].Id, "US-AUS", "Austin HQ", "+1 737 555 0101", "austin@acme.com", "Congress Ave 200, Austin", true),
            new(Guid.Parse("d4d4d4d4-2222-3333-4444-555555555555"), companies[2].Id, "AR-CABA", "Buenos Aires Centro", "+54 11 5555 0101", "caba@globex.com", "Av. Libertador 4040, CABA", false)
        };

        var paymentTerms = new List<PaymentTerm>
        {
            new(Guid.Parse("01010101-0101-0101-0101-010101010101"), "NET30", "Pago a 30 días", 30, "Crédito estándar"),
            new(Guid.Parse("02020202-0202-0202-0202-020202020202"), "CONTADO", "Contado", 0, "Pago inmediato"),
            new(Guid.Parse("03030303-0303-0303-0303-030303030303"), "NET15", "Pago a 15 días", 15, "Crédito ágil")
        };

        var currencies = new List<Currency>
        {
            new(Guid.Parse("10000000-0000-0000-0000-000000000001"), "USD", "Dólar estadounidense", "$", 2),
            new(Guid.Parse("10000000-0000-0000-0000-000000000002"), "MXN", "Peso mexicano", "$", 2),
            new(Guid.Parse("10000000-0000-0000-0000-000000000003"), "ARS", "Peso argentino", "$", 2)
        };

        var languages = new List<Language>
        {
            new("es-MX", "Español (México)", true),
            new("en-US", "English (US)", false),
            new("es-AR", "Español (Argentina)", false)
        };

        var industries = new List<Industry>
        {
            new(Guid.Parse("20000000-0000-0000-0000-000000000001"), "HOTEL", "Hospitalidad", "Hoteles, resorts y centros vacacionales"),
            new(Guid.Parse("20000000-0000-0000-0000-000000000002"), "RETAIL", "Retail", "Tiendas físicas y online"),
            new(Guid.Parse("20000000-0000-0000-0000-000000000003"), "DISTRIB", "Distribución", "Mayoristas y logística")
        };

        var segments = new List<CustomerSegment>
        {
            new(Guid.Parse("30000000-0000-0000-0000-000000000001"), companies[0].Id, "ENT", "Enterprise", "Grandes cuentas con procesos complejos", true),
            new(Guid.Parse("30000000-0000-0000-0000-000000000002"), companies[1].Id, "PREM", "Premium", "Clientes estratégicos", true),
            new(Guid.Parse("30000000-0000-0000-0000-000000000003"), companies[2].Id, "PYME", "PyME", "Pequeñas y medianas empresas", true)
        };

        var customers = new List<Customer>
        {
            new(Guid.Parse("99990000-0000-0000-0000-000000000001"), companies[0].Id, "C-1001", "Hotel Primavera", "MXN", "NET30", 50000m, false, "Enterprise", "MX-HQ", "es-MX"),
            new(Guid.Parse("99990000-0000-0000-0000-000000000002"), companies[1].Id, "C-2001", "Globex Retail", "USD", "NET15", 25000m, false, "Premium", "US-AUS", "en-US"),
            new(Guid.Parse("99990000-0000-0000-0000-000000000003"), companies[2].Id, "C-3001", "Distribuciones Andinas", "ARS", "CONTADO", 8000m, true, "PyME", "AR-CABA", "es-AR")
        };

        var users = new List<UserAccount>
        {
            new(Guid.Parse("11111111-1111-1111-1111-111111111111"), "admin@acme.test", "ACME", new []{"admin","ops"}, true),
            new(Guid.Parse("22222222-2222-2222-2222-222222222222"), "seller@globex.test", "GLOB", new []{"sales"}, true),
            new(Guid.Parse("33333333-3333-3333-3333-333333333333"), "ops@globex.test", "GLOB", new []{"ops"}, false)
        };

        store
            .Seed(tenants)
            .Seed(companies)
            .Seed(branches)
            .Seed(paymentTerms)
            .Seed(currencies)
            .Seed(languages)
            .Seed(industries)
            .Seed(segments)
            .Seed(customers)
            .Seed(users);

        return store;
    }
}
