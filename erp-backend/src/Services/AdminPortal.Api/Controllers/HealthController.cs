using Microsoft.AspNetCore.Mvc;

namespace AdminPortal.Api.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    /// <summary>
    /// Verifica que el API esté disponible.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get() => Ok(new { status = "ok", message = "Admin Portal API ready" });
}
