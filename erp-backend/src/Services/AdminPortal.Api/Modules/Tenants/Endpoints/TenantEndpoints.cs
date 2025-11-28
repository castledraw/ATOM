using AdminPortal.Api.Modules.Tenants.Queries;
using Microsoft.AspNetCore.Routing;

namespace AdminPortal.Api.Modules.Tenants.Endpoints;

public static class TenantEndpoints
{
    public static IEndpointRouteBuilder MapTenantEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tenants").WithTags("Tenants");
        group.MapGet("/", () => Results.Ok(GetTenants.Handle()))
            .WithSummary("Lista tenants mock")
            .WithDescription("Endpoint temporal antes de integrar CQRS + multi-tenancy");

        // TODO: mapear comandos/queries adicionales (crear, actualizar, suspender tenants)
        return app;
    }
}
