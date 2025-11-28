using AdminPortal.Api.Modules.Catalogs.Queries;
using ERP.Shared.Application.CQRS;
using Microsoft.AspNetCore.Routing;

namespace AdminPortal.Api.Modules.Catalogs.Endpoints;

public static class CatalogEndpoints
{
    public static IEndpointRouteBuilder MapCatalogEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/catalogs").WithTags("Catalogs");

        group.MapGet("/currencies", async (IQueryHandler<GetCurrenciesQuery, IReadOnlyList<Modules.Catalogs.Models.CurrencyDto>> handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(new GetCurrenciesQuery(), ct);
            return Results.Ok(result);
        })
            .WithSummary("Catálogo de monedas");

        group.MapGet("/payment-terms", async (IQueryHandler<GetPaymentTermsQuery, IReadOnlyList<Modules.Catalogs.Models.PaymentTermDto>> handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(new GetPaymentTermsQuery(), ct);
            return Results.Ok(result);
        })
            .WithSummary("Catálogo de términos de pago");

        group.MapGet("/languages", async (IQueryHandler<GetLanguagesQuery, IReadOnlyList<Modules.Catalogs.Models.LanguageDto>> handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(new GetLanguagesQuery(), ct);
            return Results.Ok(result);
        })
            .WithSummary("Idiomas soportados por tenant");

        group.MapGet("/industries", async (IQueryHandler<GetIndustriesQuery, IReadOnlyList<Modules.Catalogs.Models.IndustryDto>> handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(new GetIndustriesQuery(), ct);
            return Results.Ok(result);
        })
            .WithSummary("Catálogo de industrias ERP");

        return app;
    }
}
