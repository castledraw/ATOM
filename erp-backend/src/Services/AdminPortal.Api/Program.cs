using AdminPortal.Api.Modules.Catalogs.Endpoints;
using AdminPortal.Api.Modules.Catalogs.Queries;
using AdminPortal.Api.Modules.Companies.Endpoints;
using AdminPortal.Api.Modules.Companies.Queries;
using AdminPortal.Api.Modules.Tenants.Endpoints;
using AdminPortal.Api.Modules.Tenants.Queries;
using ERP.Shared.Application.CQRS;
using ERP.Shared.Infrastructure.InMemory;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedInMemoryReadModels();
builder.Services.AddScoped<IQueryHandler<GetTenantsQuery, IReadOnlyList<AdminPortal.Api.Modules.Tenants.Models.TenantSummary>>, GetTenantsHandler>();
builder.Services.AddScoped<IQueryHandler<GetTenantDetailQuery, AdminPortal.Api.Modules.Tenants.Models.TenantSummary?>, GetTenantDetailHandler>();
builder.Services.AddScoped<IQueryHandler<GetTenantCompaniesQuery, IReadOnlyList<AdminPortal.Api.Modules.Companies.Models.CompanySummary>>, GetTenantCompaniesHandler>();

builder.Services.AddScoped<IQueryHandler<GetCompaniesQuery, IReadOnlyList<AdminPortal.Api.Modules.Companies.Models.CompanySummary>>, GetCompaniesHandler>();
builder.Services.AddScoped<IQueryHandler<GetCompanyDetailQuery, AdminPortal.Api.Modules.Companies.Models.CompanySummary?>, GetCompanyDetailHandler>();
builder.Services.AddScoped<IQueryHandler<GetCompanyBranchesQuery, IReadOnlyList<AdminPortal.Api.Modules.Companies.Models.CompanyBranchSummary>>, GetCompanyBranchesHandler>();
builder.Services.AddScoped<IQueryHandler<GetCompanyCustomersQuery, IReadOnlyList<AdminPortal.Api.Modules.Companies.Models.CustomerSummary>>, GetCompanyCustomersHandler>();
builder.Services.AddScoped<IQueryHandler<GetCompanySegmentsQuery, IReadOnlyList<AdminPortal.Api.Modules.Companies.Models.CustomerSegmentDto>>, GetCompanySegmentsHandler>();

builder.Services.AddScoped<IQueryHandler<GetCurrenciesQuery, IReadOnlyList<AdminPortal.Api.Modules.Catalogs.Models.CurrencyDto>>, GetCurrenciesHandler>();
builder.Services.AddScoped<IQueryHandler<GetPaymentTermsQuery, IReadOnlyList<AdminPortal.Api.Modules.Catalogs.Models.PaymentTermDto>>, GetPaymentTermsHandler>();
builder.Services.AddScoped<IQueryHandler<GetLanguagesQuery, IReadOnlyList<AdminPortal.Api.Modules.Catalogs.Models.LanguageDto>>, GetLanguagesHandler>();
builder.Services.AddScoped<IQueryHandler<GetIndustriesQuery, IReadOnlyList<AdminPortal.Api.Modules.Catalogs.Models.IndustryDto>>, GetIndustriesHandler>();

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
