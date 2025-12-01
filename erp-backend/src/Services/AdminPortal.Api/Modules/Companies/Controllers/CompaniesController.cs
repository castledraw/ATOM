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
    /// <summary>
    /// Lista de empresas.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CompanySummary>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CompanySummary>>> GetCompanies(
        [FromServices] IQueryHandler<GetCompaniesQuery, IReadOnlyList<CompanySummary>> handler,
        CancellationToken cancellationToken)
    {
        var companies = await handler.Handle(new GetCompaniesQuery(), cancellationToken);
        return Ok(companies);
    }

    /// <summary>
    /// Detalle de empresa.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CompanySummary), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanySummary>> GetById(
        Guid id,
        [FromServices] IQueryHandler<GetCompanyDetailQuery, CompanySummary?> handler,
        CancellationToken cancellationToken)
    {
        var company = await handler.Handle(new GetCompanyDetailQuery(id), cancellationToken);
        return company is null ? NotFound() : Ok(company);
    }

    /// <summary>
    /// Sucursales por empresa.
    /// </summary>
    [HttpGet("{id:guid}/branches")]
    [ProducesResponseType(typeof(IReadOnlyList<CompanyBranchSummary>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CompanyBranchSummary>>> GetBranches(
        Guid id,
        [FromServices] IQueryHandler<GetCompanyBranchesQuery, IReadOnlyList<CompanyBranchSummary>> handler,
        CancellationToken cancellationToken)
    {
        var branches = await handler.Handle(new GetCompanyBranchesQuery(id), cancellationToken);
        return Ok(branches);
    }

    /// <summary>
    /// Clientes por empresa.
    /// </summary>
    [HttpGet("{id:guid}/customers")]
    [ProducesResponseType(typeof(IReadOnlyList<CustomerSummary>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CustomerSummary>>> GetCustomers(
        Guid id,
        [FromServices] IQueryHandler<GetCompanyCustomersQuery, IReadOnlyList<CustomerSummary>> handler,
        CancellationToken cancellationToken)
    {
        var customers = await handler.Handle(new GetCompanyCustomersQuery(id), cancellationToken);
        return Ok(customers);
    }

    /// <summary>
    /// Segmentos comerciales por empresa.
    /// </summary>
    [HttpGet("{id:guid}/segments")]
    [ProducesResponseType(typeof(IReadOnlyList<CustomerSegmentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CustomerSegmentDto>>> GetSegments(
        Guid id,
        [FromServices] IQueryHandler<GetCompanySegmentsQuery, IReadOnlyList<CustomerSegmentDto>> handler,
        CancellationToken cancellationToken)
    {
        var segments = await handler.Handle(new GetCompanySegmentsQuery(id), cancellationToken);
        return Ok(segments);
    }
}
