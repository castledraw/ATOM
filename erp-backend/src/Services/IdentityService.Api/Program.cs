var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "ok", service = "IdentityService" }));
// TODO: configurar ASP.NET Identity + Cognito federación y endpoints de autenticación

app.Run();
