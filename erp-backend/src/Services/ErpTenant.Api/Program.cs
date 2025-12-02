using System.Reflection;
using ErpTenant.Api.Customers;
using ErpTenant.Api.Customers.Commands;
using ErpTenant.Api.Customers.Queries;
using ERP.Shared.Application.CQRS;
using ERP.Shared.Infrastructure.InMemory;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedInMemoryRepositories();
builder.Services.AddScoped<IQueryHandler<GetCustomersQuery, IReadOnlyList<CustomerDetail>>, GetCustomersHandler>();
builder.Services.AddScoped<IQueryHandler<GetCustomerByIdQuery, CustomerDetail?>, GetCustomerByIdHandler>();
builder.Services.AddScoped<ICommandHandler<CreateCustomerCommand, CustomerDetail>, CustomerCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateCustomerCreditCommand, CustomerDetail?>, CustomerCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateCustomerProfileCommand, CustomerDetail?>, CustomerCommandHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ERP Tenant API", Version = "v1" });
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
