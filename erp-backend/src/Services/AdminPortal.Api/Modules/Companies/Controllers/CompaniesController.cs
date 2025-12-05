using AdminPortal.Api.Modules.Companies.Commands;
using AdminPortal.Api.Modules.Companies.Models;
using AdminPortal.Api.Modules.Companies.Queries;
using ERP.Shared.Application.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace AdminPortal.Api.Modules.Companies.Controllers;

[ApiController]
[Route("api/companies")]
[Produces("application/json")]
public class CompaniesController : ControllerBase
{
    private readonly IQueryHandler<GetCompaniesQuery, IReadOnlyList<CompanySummary>> _listHandler;
    private readonly IQueryHandler<GetCompanyDetailQuery, CompanySummary?> _detailHandler;
    private readonly IQueryHandler<GetCompanyBranchesQuery, IReadOnlyList<CompanyBranchSummary>> _branchesHandler;
    private readonly IQueryHandler<GetCompanyCustomersQuery, IReadOnlyList<CustomerSummary>> _customersHandler;
    private readonly IQueryHandler<GetCompanySegmentsQuery, IReadOnlyList<CustomerSegmentDto>> _segmentsHandler;
    private readonly ICommandHandler<CreateCompanyCommand, CompanySummary> _createHandler;
    private readonly ICommandHandler<UpdateCompanyCommand, CompanySummary?> _updateHandler;
    private readonly ICommandHandler<RegisterBranchCommand, CompanyBranchSummary> _registerBranchHandler;
    private readonly ICommandHandler<ToggleCompanyStatusCommand, CompanySummary?> _toggleStatusHandler;

    public CompaniesController(
        IQueryHandler<GetCompaniesQuery, IReadOnlyList<CompanySummary>> listHandler,
        IQueryHandler<GetCompanyDetailQuery, CompanySummary?> detailHandler,
        IQueryHandler<GetCompanyBranchesQuery, IReadOnlyList<CompanyBranchSummary>> branchesHandler,
        IQueryHandler<GetCompanyCustomersQuery, IReadOnlyList<CustomerSummary>> customersHandler,
        IQueryHandler<GetCompanySegmentsQuery, IReadOnlyList<CustomerSegmentDto>> segmentsHandler,
        ICommandHandler<CreateCompanyCommand, CompanySummary> createHandler,
        ICommandHandler<UpdateCompanyCommand, CompanySummary?> updateHandler,
        ICommandHandler<RegisterBranchCommand, CompanyBranchSummary> registerBranchHandler,
        ICommandHandler<ToggleCompanyStatusCommand, CompanySummary?> toggleStatusHandler)
    {
        _listHandler = listHandler;
        _detailHandler = detailHandler;
        _branchesHandler = branchesHandler;
        _customersHandler = customersHandler;
        _segmentsHandler = segmentsHandler;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _registerBranchHandler = registerBranchHandler;
        _toggleStatusHandler = toggleStatusHandler;
    }

    /// <summary>
    /// Lista de empresas.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CompanySummary>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CompanySummary>>> GetCompanies(CancellationToken cancellationToken)
    {
        var companies = await _listHandler.Handle(new GetCompaniesQuery(), cancellationToken);
        return Ok(companies);
    }

    /// <summary>
    /// Detalle de empresa.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CompanySummary), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanySummary>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var company = await _detailHandler.Handle(new GetCompanyDetailQuery(id), cancellationToken);
        return company is null ? NotFound() : Ok(company);
    }

    /// <summary>
    /// Sucursales por empresa.
    /// </summary>
    [HttpGet("{id:guid}/branches")]
    [ProducesResponseType(typeof(IReadOnlyList<CompanyBranchSummary>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CompanyBranchSummary>>> GetBranches(Guid id, CancellationToken cancellationToken)
    {
        var branches = await _branchesHandler.Handle(new GetCompanyBranchesQuery(id), cancellationToken);
        return Ok(branches);
    }

    /// <summary>
    /// Clientes por empresa.
    /// </summary>
    [HttpGet("{id:guid}/customers")]
    [ProducesResponseType(typeof(IReadOnlyList<CustomerSummary>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CustomerSummary>>> GetCustomers(Guid id, CancellationToken cancellationToken)
    {
        var customers = await _customersHandler.Handle(new GetCompanyCustomersQuery(id), cancellationToken);
        return Ok(customers);
    }

    /// <summary>
    /// Segmentos comerciales por empresa.
    /// </summary>
    [HttpGet("{id:guid}/segments")]
    [ProducesResponseType(typeof(IReadOnlyList<CustomerSegmentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CustomerSegmentDto>>> GetSegments(Guid id, CancellationToken cancellationToken)
    {
        var segments = await _segmentsHandler.Handle(new GetCompanySegmentsQuery(id), cancellationToken);
        return Ok(segments);
    }

    /// <summary>
    /// Crea una empresa dentro de un tenant.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CompanySummary), StatusCodes.Status201Created)]
    public async Task<ActionResult<CompanySummary>> Create([FromBody] CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        var company = await _createHandler.Handle(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = company.Id }, company);
    }

    /// <summary>
    /// Actualiza la configuración base de una empresa.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CompanySummary), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanySummary>> Update(Guid id, [FromBody] UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        var company = await _updateHandler.Handle(command with { Id = id }, cancellationToken);
        return company is null ? NotFound() : Ok(company);
    }

    /// <summary>
    /// Registra una sucursal para la empresa.
    /// </summary>
    [HttpPost("{id:guid}/branches")]
    [ProducesResponseType(typeof(CompanyBranchSummary), StatusCodes.Status201Created)]
    public async Task<ActionResult<CompanyBranchSummary>> CreateBranch(Guid id, [FromBody] RegisterBranchCommand command, CancellationToken cancellationToken)
    {
        var branch = await _registerBranchHandler.Handle(command with { CompanyId = id }, cancellationToken);
        return CreatedAtAction(nameof(GetBranches), new { id }, branch);
    }

    /// <summary>
    /// Activa o desactiva una empresa.
    /// </summary>
    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(typeof(CompanySummary), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanySummary>> ToggleStatus(Guid id, [FromBody] ToggleCompanyStatusCommand command, CancellationToken cancellationToken)
    {
        var updated = await _toggleStatusHandler.Handle(command with { CompanyId = id }, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }
}
