using AdminPortal.Api.Modules.Tenants.Models;
using AdminPortal.Api.Modules.Tenants.Queries;
using ERP.Shared.Application.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace AdminPortal.Api.Modules.Tenants.Controllers;

[ApiController]
[Route("api/tenants")]
[Produces("application/json")]
public class TenantsController : ControllerBase
{
    /// <summary>
    /// Lista de tenants con idioma, dominios y conteo de empresas.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TenantSummary>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TenantSummary>>> GetTenants(
        [FromServices] IQueryHandler<GetTenantsQuery, IReadOnlyList<TenantSummary>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetTenantsQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Obtiene el detalle de un tenant.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TenantSummary), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TenantSummary>> GetById(
        Guid id,
        [FromServices] IQueryHandler<GetTenantDetailQuery, TenantSummary?> handler,
        CancellationToken cancellationToken)
    {
        var tenant = await handler.Handle(new GetTenantDetailQuery(id), cancellationToken);
        return tenant is null ? NotFound() : Ok(tenant);
    }

    /// <summary>
    /// Empresas asociadas a un tenant.
    /// </summary>
    [HttpGet("{id:guid}/companies")]
    [ProducesResponseType(typeof(IReadOnlyList<Modules.Companies.Models.CompanySummary>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<Modules.Companies.Models.CompanySummary>>> GetCompanies(
        Guid id,
        [FromServices] IQueryHandler<GetTenantCompaniesQuery, IReadOnlyList<Modules.Companies.Models.CompanySummary>> handler,
        CancellationToken cancellationToken)
    {
        var companies = await handler.Handle(new GetTenantCompaniesQuery(id), cancellationToken);
        return Ok(companies);
    }
}
