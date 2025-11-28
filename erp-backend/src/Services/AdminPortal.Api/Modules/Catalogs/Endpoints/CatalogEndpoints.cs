using AdminPortal.Api.Data;
using Microsoft.AspNetCore.Routing;

namespace AdminPortal.Api.Modules.Catalogs.Endpoints;

public static class CatalogEndpoints
{
    public static IEndpointRouteBuilder MapCatalogEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/catalogs").WithTags("Catalogs");

        group.MapGet("/currencies", () => Results.Ok(AdminPortalData.Currencies))
            .WithSummary("Catálogo de monedas");

        group.MapGet("/payment-terms", () => Results.Ok(AdminPortalData.PaymentTerms))
            .WithSummary("Catálogo de términos de pago");

        group.MapGet("/languages", () => Results.Ok(AdminPortalData.Languages))
            .WithSummary("Idiomas soportados por tenant");

        group.MapGet("/industries", () => Results.Ok(AdminPortalData.Industries))
            .WithSummary("Catálogo de industrias ERP");

        return app;
    }
}
