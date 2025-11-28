using AdminPortal.Api.Data;
using Microsoft.AspNetCore.Routing;

namespace AdminPortal.Api.Modules.Tenants.Endpoints;

public static class TenantEndpoints
{
    public static IEndpointRouteBuilder MapTenantEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tenants").WithTags("Tenants");

        group.MapGet("/", () => Results.Ok(AdminPortalData.Tenants))
            .WithSummary("Lista tenants ERP-grade").WithDescription("Incluye idioma, dominios y conteo de empresas");

        group.MapGet("/{id:guid}/companies", (Guid id) =>
        {
            var companies = AdminPortalData.Companies.Where(c => c.TenantId == id);
            return Results.Ok(companies);
        }).WithSummary("Empresas por tenant");

        group.MapGet("/{id:guid}", (Guid id) =>
        {
            var tenant = AdminPortalData.Tenants.FirstOrDefault(t => t.Id == id);
            return tenant is null ? Results.NotFound() : Results.Ok(tenant);
        }).WithSummary("Detalle de tenant");

        return app;
    }
}
