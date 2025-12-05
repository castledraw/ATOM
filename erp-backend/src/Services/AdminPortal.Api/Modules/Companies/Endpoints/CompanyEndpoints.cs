using AdminPortal.Api.Modules.Companies.Models;
using AdminPortal.Api.Modules.Companies.Queries;
using ERP.Shared.Application.CQRS;
using Microsoft.AspNetCore.Routing;

namespace AdminPortal.Api.Modules.Companies.Endpoints;

public static class CompanyEndpoints
{
    public static IEndpointRouteBuilder MapCompanyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/companies").WithTags("Companies");

        group.MapGet("/", async (IQueryHandler<GetCompaniesQuery, IReadOnlyList<CompanySummary>> handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(new GetCompaniesQuery(), ct);
            return Results.Ok(result);
        })
        .WithSummary("Lista empresas")
        .WithDescription("Devuelve empresas por tenant con catálogo básico listo para CRUD");

        group.MapGet("/{id:guid}/branches", async (Guid id, IQueryHandler<GetCompanyBranchesQuery, IReadOnlyList<CompanyBranchSummary>> handler, CancellationToken ct) =>
        {
            var branches = await handler.Handle(new GetCompanyBranchesQuery(id), ct);
            return Results.Ok(branches);
        })
        .WithSummary("Sucursales por empresa")
        .WithDescription("Mock multi-sucursal alineado al modelo tenant/company/branch");

        group.MapGet("/{id:guid}/customers", async (Guid id, IQueryHandler<GetCompanyCustomersQuery, IReadOnlyList<CustomerSummary>> handler, CancellationToken ct) =>
        {
            var customers = await handler.Handle(new GetCompanyCustomersQuery(id), ct);
            return Results.Ok(customers);
        })
        .WithSummary("Clientes por empresa")
        .WithDescription("Mock ERP-grade para ver términos de pago, moneda, crédito y segmentación");

        group.MapGet("/{id:guid}", async (Guid id, IQueryHandler<GetCompanyDetailQuery, CompanySummary?> handler, CancellationToken ct) =>
        {
            var company = await handler.Handle(new GetCompanyDetailQuery(id), ct);
            return company is null ? Results.NotFound() : Results.Ok(company);
        })
        .WithSummary("Detalle de empresa");

        group.MapGet("/{id:guid}/segments", async (Guid id, IQueryHandler<GetCompanySegmentsQuery, IReadOnlyList<CustomerSegmentDto>> handler, CancellationToken ct) =>
        {
            var segments = await handler.Handle(new GetCompanySegmentsQuery(id), ct);
            return Results.Ok(segments);
        })
        .WithSummary("Segmentos de cliente por empresa");

        return app;
    }
}
