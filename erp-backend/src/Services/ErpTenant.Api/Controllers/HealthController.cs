using Microsoft.AspNetCore.Mvc;

namespace ErpTenant.Api.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    /// <summary>
    /// Verifica que el API de tenant responde.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get() => Ok(new { status = "ok", message = "Tenant API ready" });
}
