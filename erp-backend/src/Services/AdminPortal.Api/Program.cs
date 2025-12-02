using AdminPortal.Api.Modules.Catalogs.Commands;
using AdminPortal.Api.Modules.Catalogs.Queries;
using AdminPortal.Api.Modules.Companies.Commands;
using AdminPortal.Api.Modules.Companies.Queries;
using AdminPortal.Api.Modules.Tenants.Commands;
using AdminPortal.Api.Modules.Tenants.Queries;
using System.Reflection;
using ERP.Shared.Application.CQRS;
using ERP.Shared.Infrastructure.InMemory;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedInMemoryRepositories();
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

builder.Services.AddScoped<ICommandHandler<CreateTenantCommand, AdminPortal.Api.Modules.Tenants.Models.TenantSummary>, TenantCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateTenantCommand, AdminPortal.Api.Modules.Tenants.Models.TenantSummary?>, TenantCommandHandler>();
builder.Services.AddScoped<ICommandHandler<SetTenantStatusCommand, AdminPortal.Api.Modules.Tenants.Models.TenantSummary?>, TenantCommandHandler>();

builder.Services.AddScoped<ICommandHandler<CreateCompanyCommand, AdminPortal.Api.Modules.Companies.Models.CompanySummary>, CompanyCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateCompanyCommand, AdminPortal.Api.Modules.Companies.Models.CompanySummary?>, CompanyCommandHandler>();
builder.Services.AddScoped<ICommandHandler<RegisterBranchCommand, AdminPortal.Api.Modules.Companies.Models.CompanyBranchSummary>, CompanyCommandHandler>();
builder.Services.AddScoped<ICommandHandler<ToggleCompanyStatusCommand, AdminPortal.Api.Modules.Companies.Models.CompanySummary?>, CompanyCommandHandler>();

builder.Services.AddScoped<ICommandHandler<CreateCurrencyCommand, AdminPortal.Api.Modules.Catalogs.Models.CurrencyDto>, CatalogCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreatePaymentTermCommand, AdminPortal.Api.Modules.Catalogs.Models.PaymentTermDto>, CatalogCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateLanguageCommand, AdminPortal.Api.Modules.Catalogs.Models.LanguageDto>, CatalogCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateIndustryCommand, AdminPortal.Api.Modules.Catalogs.Models.IndustryDto>, CatalogCommandHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Admin Portal API", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    }
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
