using ErpTenant.Api.Customers;
using ErpTenant.Api.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ERP Tenant API", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/health", () => Results.Ok(new { status = "ok", message = "Tenant API ready" }));

app.MapGet("/customers", (string? tenantCode, string? companyCode, string? branchCode) =>
{
    var query = TenantPortalData.Customers.AsQueryable();
    if (!string.IsNullOrWhiteSpace(tenantCode))
    {
        query = query.Where(c => c.TenantCode.Equals(tenantCode, StringComparison.OrdinalIgnoreCase));
    }
    if (!string.IsNullOrWhiteSpace(companyCode))
    {
        query = query.Where(c => c.CompanyCode.Equals(companyCode, StringComparison.OrdinalIgnoreCase));
    }
    if (!string.IsNullOrWhiteSpace(branchCode))
    {
        query = query.Where(c => string.Equals(c.BranchCode, branchCode, StringComparison.OrdinalIgnoreCase));
    }
    return Results.Ok(query);
}).WithSummary("Listado de clientes por tenant/empresa");

app.MapGet("/customers/{id:guid}", (Guid id) =>
{
    var customer = TenantPortalData.Customers.FirstOrDefault(c => c.Id == id);
    return customer is null ? Results.NotFound() : Results.Ok(customer);
}).WithSummary("Detalle de cliente");

app.Run();
