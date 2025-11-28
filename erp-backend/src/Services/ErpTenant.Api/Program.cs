using ErpTenant.Api.Customers;
using ErpTenant.Api.Customers.Queries;
using ERP.Shared.Application.CQRS;
using ERP.Shared.Infrastructure.InMemory;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedInMemoryReadModels();
builder.Services.AddScoped<IQueryHandler<GetCustomersQuery, IReadOnlyList<CustomerDetail>>, GetCustomersHandler>();
builder.Services.AddScoped<IQueryHandler<GetCustomerByIdQuery, CustomerDetail?>, GetCustomerByIdHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ERP Tenant API", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/health", () => Results.Ok(new { status = "ok", message = "Tenant API ready" }));

app.MapGet("/customers", async (string? tenantCode, string? companyCode, string? branchCode, IQueryHandler<GetCustomersQuery, IReadOnlyList<CustomerDetail>> handler, CancellationToken ct) =>
{
    var result = await handler.Handle(new GetCustomersQuery(tenantCode, companyCode, branchCode), ct);
    return Results.Ok(result);
}).WithSummary("Listado de clientes por tenant/empresa");

app.MapGet("/customers/{id:guid}", async (Guid id, IQueryHandler<GetCustomerByIdQuery, CustomerDetail?> handler, CancellationToken ct) =>
{
    var customer = await handler.Handle(new GetCustomerByIdQuery(id), ct);
    return customer is null ? Results.NotFound() : Results.Ok(customer);
}).WithSummary("Detalle de cliente");

app.Run();
