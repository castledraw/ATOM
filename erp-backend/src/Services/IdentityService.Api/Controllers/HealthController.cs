using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    /// <summary>
    /// Verifica que el servicio de identidad está disponible.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get() => Ok(new { status = "ok", message = "Identity service ready" });
}
