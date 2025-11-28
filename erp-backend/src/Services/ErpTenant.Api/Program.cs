var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "ok", service = "ErpTenant" }));
// TODO: exponer vertical slices para módulos ERP orientados al tenant, incluir filtros multi-tenant y autorización por roles

app.Run();
