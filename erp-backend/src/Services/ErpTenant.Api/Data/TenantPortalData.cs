namespace ErpTenant.Api.Data;

using ErpTenant.Api.Customers;

public static class TenantPortalData
{
    public static IReadOnlyList<CustomerDetail> Customers => new List<CustomerDetail>
    {
        new(Guid.Parse("99990000-0000-0000-0000-000000000001"), "C-1001", "Hotel Primavera", "MXN", "NET30", 50000m, false, "Enterprise", "ACME-MX", "ACME"),
        new(Guid.Parse("99990000-0000-0000-0000-000000000002"), "C-2001", "Globex Retail", "USD", "NET15", 25000m, false, "Premium", "ACME-US", "ACME"),
        new(Guid.Parse("99990000-0000-0000-0000-000000000003"), "C-3001", "Distribuciones Andinas", "ARS", "CONTADO", 8000m, true, "PyME", "GLOB-AR", "GLOB")
    };
}
