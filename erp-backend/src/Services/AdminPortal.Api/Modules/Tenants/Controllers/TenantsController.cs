using AdminPortal.Api.Modules.Tenants.Commands;
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
    private readonly IQueryHandler<GetTenantsQuery, IReadOnlyList<TenantSummary>> _listHandler;
    private readonly IQueryHandler<GetTenantDetailQuery, TenantSummary?> _detailHandler;
    private readonly IQueryHandler<GetTenantCompaniesQuery, IReadOnlyList<Modules.Companies.Models.CompanySummary>> _companiesHandler;
    private readonly ICommandHandler<CreateTenantCommand, TenantSummary> _createHandler;
    private readonly ICommandHandler<UpdateTenantCommand, TenantSummary?> _updateHandler;
    private readonly ICommandHandler<SetTenantStatusCommand, TenantSummary?> _statusHandler;

    public TenantsController(
        IQueryHandler<GetTenantsQuery, IReadOnlyList<TenantSummary>> listHandler,
        IQueryHandler<GetTenantDetailQuery, TenantSummary?> detailHandler,
        IQueryHandler<GetTenantCompaniesQuery, IReadOnlyList<Modules.Companies.Models.CompanySummary>> companiesHandler,
        ICommandHandler<CreateTenantCommand, TenantSummary> createHandler,
        ICommandHandler<UpdateTenantCommand, TenantSummary?> updateHandler,
        ICommandHandler<SetTenantStatusCommand, TenantSummary?> statusHandler)
    {
        _listHandler = listHandler;
        _detailHandler = detailHandler;
        _companiesHandler = companiesHandler;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _statusHandler = statusHandler;
    }

    /// <summary>
    /// Lista de tenants con idioma, dominios y conteo de empresas.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TenantSummary>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TenantSummary>>> GetTenants(CancellationToken cancellationToken)
    {
        var result = await _listHandler.Handle(new GetTenantsQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Obtiene el detalle de un tenant.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TenantSummary), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TenantSummary>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var tenant = await _detailHandler.Handle(new GetTenantDetailQuery(id), cancellationToken);
        return tenant is null ? NotFound() : Ok(tenant);
    }

    /// <summary>
    /// Empresas asociadas a un tenant.
    /// </summary>
    [HttpGet("{id:guid}/companies")]
    [ProducesResponseType(typeof(IReadOnlyList<Modules.Companies.Models.CompanySummary>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<Modules.Companies.Models.CompanySummary>>> GetCompanies(
        Guid id,
        CancellationToken cancellationToken)
    {
        var companies = await _companiesHandler.Handle(new GetTenantCompaniesQuery(id), cancellationToken);
        return Ok(companies);
    }

    /// <summary>
    /// Crea un nuevo tenant multi-tenant listo para despliegue.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(TenantSummary), StatusCodes.Status201Created)]
    public async Task<ActionResult<TenantSummary>> Create([FromBody] CreateTenantCommand command, CancellationToken cancellationToken)
    {
        var created = await _createHandler.Handle(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Actualiza nombre, dominio, idioma y estatus de un tenant.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(TenantSummary), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TenantSummary>> Update(Guid id, [FromBody] UpdateTenantCommand command, CancellationToken cancellationToken)
    {
        var result = await _updateHandler.Handle(command with { Id = id }, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Suspende o reactiva un tenant.
    /// </summary>
    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(typeof(TenantSummary), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TenantSummary>> UpdateStatus(Guid id, [FromBody] SetTenantStatusCommand command, CancellationToken cancellationToken)
    {
        var result = await _statusHandler.Handle(command with { Id = id }, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }
}
