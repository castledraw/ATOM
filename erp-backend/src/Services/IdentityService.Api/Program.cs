using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity Service API", Version = "v1" });
});

var app = builder.Build();

var users = new[]
{
    new { Id = Guid.NewGuid(), Email = "admin@acme.test", Tenant = "ACME", Roles = new []{"admin","ops"}},
    new { Id = Guid.NewGuid(), Email = "seller@globex.test", Tenant = "GLOB", Roles = new []{"sales"}},
};

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/health", () => Results.Ok(new { status = "ok", message = "Identity service ready" }));
app.MapGet("/users", () => Results.Ok(users)).WithSummary("Usuarios mock");

app.Run();
