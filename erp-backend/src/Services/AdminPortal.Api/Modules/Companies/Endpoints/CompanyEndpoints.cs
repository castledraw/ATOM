using AdminPortal.Api.Data;
using AdminPortal.Api.Modules.Companies.Models;
using Microsoft.AspNetCore.Routing;

namespace AdminPortal.Api.Modules.Companies.Endpoints;

public static class CompanyEndpoints
{
    public static IEndpointRouteBuilder MapCompanyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/companies").WithTags("Companies");

        group.MapGet("/", () => Results.Ok(AdminPortalData.Companies))
            .WithSummary("Lista empresas")
            .WithDescription("Devuelve empresas por tenant con catálogo básico listo para CRUD");

        group.MapGet("/{id:guid}/branches", (Guid id) =>
        {
            var branches = AdminPortalData.Branches.Where(b => b.CompanyId == id);
            return Results.Ok(branches);
        })
        .WithSummary("Sucursales por empresa")
        .WithDescription("Mock multi-sucursal alineado al modelo tenant/company/branch");

        group.MapGet("/{id:guid}/customers", (Guid id) =>
        {
            var customers = AdminPortalData.Customers.Where(c => c.CompanyId == id);
            return Results.Ok(customers);
        })
        .WithSummary("Clientes por empresa")
        .WithDescription("Mock ERP-grade para ver términos de pago, moneda, crédito y segmentación");

        group.MapGet("/{id:guid}", (Guid id) =>
        {
            var company = AdminPortalData.Companies.FirstOrDefault(c => c.Id == id);
            return company is null ? Results.NotFound() : Results.Ok(company);
        })
        .WithSummary("Detalle de empresa");

        group.MapGet("/{id:guid}/segments", (Guid id) =>
        {
            var segments = AdminPortalData.CustomerSegments.Where(s => s.CompanyId == id);
            return Results.Ok(segments);
        })
        .WithSummary("Segmentos de cliente por empresa");

        return app;
    }
}
