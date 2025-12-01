using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers;

[ApiController]
[Route("api/users")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private static readonly object[] Users = new[]
    {
        new { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Email = "admin@acme.test", Tenant = "ACME", Roles = new []{"admin","ops"}},
        new { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Email = "seller@globex.test", Tenant = "GLOB", Roles = new []{"sales"}},
    };

    /// <summary>
    /// Lista de usuarios mock para pruebas de integración.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult List() => Ok(Users);
}
