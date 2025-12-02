using ErpTenant.Api.Customers;
using ErpTenant.Api.Customers.Commands;
using ErpTenant.Api.Customers.Queries;
using ERP.Shared.Application.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace ErpTenant.Api.Controllers;

[ApiController]
[Route("api/customers")]
[Produces("application/json")]
public class CustomersController : ControllerBase
{
    private readonly IQueryHandler<GetCustomersQuery, IReadOnlyList<CustomerDetail>> _listHandler;
    private readonly IQueryHandler<GetCustomerByIdQuery, CustomerDetail?> _detailHandler;
    private readonly ICommandHandler<CreateCustomerCommand, CustomerDetail> _createHandler;
    private readonly ICommandHandler<UpdateCustomerCreditCommand, CustomerDetail?> _creditHandler;
    private readonly ICommandHandler<UpdateCustomerProfileCommand, CustomerDetail?> _profileHandler;

    public CustomersController(
        IQueryHandler<GetCustomersQuery, IReadOnlyList<CustomerDetail>> listHandler,
        IQueryHandler<GetCustomerByIdQuery, CustomerDetail?> detailHandler,
        ICommandHandler<CreateCustomerCommand, CustomerDetail> createHandler,
        ICommandHandler<UpdateCustomerCreditCommand, CustomerDetail?> creditHandler,
        ICommandHandler<UpdateCustomerProfileCommand, CustomerDetail?> profileHandler)
    {
        _listHandler = listHandler;
        _detailHandler = detailHandler;
        _createHandler = createHandler;
        _creditHandler = creditHandler;
        _profileHandler = profileHandler;
    }

    /// <summary>
    /// Lista clientes filtrando por tenant, empresa o sucursal.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CustomerDetail>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CustomerDetail>>> Get(
        [FromQuery] string? tenantCode,
        [FromQuery] string? companyCode,
        [FromQuery] string? branchCode,
        CancellationToken cancellationToken)
    {
        var customers = await _listHandler.Handle(new GetCustomersQuery(tenantCode, companyCode, branchCode), cancellationToken);
        return Ok(customers);
    }

    /// <summary>
    /// Detalle de cliente por identificador.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CustomerDetail), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDetail>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var customer = await _detailHandler.Handle(new GetCustomerByIdQuery(id), cancellationToken);
        return customer is null ? NotFound() : Ok(customer);
    }

    /// <summary>
    /// Crea un cliente ERP listo para facturación.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CustomerDetail), StatusCodes.Status201Created)]
    public async Task<ActionResult<CustomerDetail>> Create([FromBody] CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var created = await _createHandler.Handle(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Actualiza crédito de un cliente.
    /// </summary>
    [HttpPatch("{id:guid}/credit")]
    [ProducesResponseType(typeof(CustomerDetail), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDetail>> UpdateCredit(Guid id, [FromBody] UpdateCustomerCreditCommand command, CancellationToken cancellationToken)
    {
        var updated = await _creditHandler.Handle(command with { Id = id }, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>
    /// Actualiza datos comerciales de un cliente.
    /// </summary>
    [HttpPatch("{id:guid}/profile")]
    [ProducesResponseType(typeof(CustomerDetail), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDetail>> UpdateProfile(Guid id, [FromBody] UpdateCustomerProfileCommand command, CancellationToken cancellationToken)
    {
        var updated = await _profileHandler.Handle(command with { Id = id }, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }
}
