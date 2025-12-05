using AdminPortal.Api.Modules.Tenants.Queries;
using ERP.Shared.Application.CQRS;
using Microsoft.AspNetCore.Routing;

namespace AdminPortal.Api.Modules.Tenants.Endpoints;

public static class TenantEndpoints
{
    public static IEndpointRouteBuilder MapTenantEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tenants").WithTags("Tenants");

        group.MapGet("/", async (IQueryHandler<GetTenantsQuery, IReadOnlyList<Modules.Tenants.Models.TenantSummary>> handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(new GetTenantsQuery(), ct);
            return Results.Ok(result);
        })
        .WithSummary("Lista tenants ERP-grade")
        .WithDescription("Incluye idioma, dominios y conteo de empresas");

        group.MapGet("/{id:guid}/companies", async (Guid id, IQueryHandler<GetTenantCompaniesQuery, IReadOnlyList<Modules.Companies.Models.CompanySummary>> handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(new GetTenantCompaniesQuery(id), ct);
            return Results.Ok(result);
        }).WithSummary("Empresas por tenant");

        group.MapGet("/{id:guid}", async (Guid id, IQueryHandler<GetTenantDetailQuery, Modules.Tenants.Models.TenantSummary?> handler, CancellationToken ct) =>
        {
            var tenant = await handler.Handle(new GetTenantDetailQuery(id), ct);
            return tenant is null ? Results.NotFound() : Results.Ok(tenant);
        }).WithSummary("Detalle de tenant");

        return app;
    }
}
