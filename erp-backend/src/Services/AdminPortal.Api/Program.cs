using AdminPortal.Api.Modules.Tenants.Endpoints;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Admin Portal API", Version = "v1" });
    // TODO: configure authentication/authorization, multi-tenant filters, CQRS pipelines, repository + UoW wiring
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapTenantEndpoints();

app.MapGet("/health", () => Results.Ok(new { status = "ok", message = "Admin Portal skeleton" }));
// TODO: register vertical slices for Tenants, Subscriptions y Deployments (Commands/Queries separados)
// TODO: integrar multi-tenancy via tenant_id y GUID v7 como PK
// TODO: conectar con Cognito/Identity y aplicar policies

app.Run();
