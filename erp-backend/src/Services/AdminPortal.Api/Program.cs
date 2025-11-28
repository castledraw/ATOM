using AdminPortal.Api.Modules.Catalogs.Endpoints;
using AdminPortal.Api.Modules.Companies.Endpoints;
using AdminPortal.Api.Modules.Tenants.Endpoints;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Admin Portal API", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/health", () => Results.Ok(new { status = "ok", message = "Admin Portal API ready" }));
app.MapTenantEndpoints();
app.MapCompanyEndpoints();
app.MapCatalogEndpoints();

app.Run();
