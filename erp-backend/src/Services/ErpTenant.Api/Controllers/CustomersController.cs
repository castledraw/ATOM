using ErpTenant.Api.Customers;
using ErpTenant.Api.Customers.Queries;
using ERP.Shared.Application.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace ErpTenant.Api.Controllers;

[ApiController]
[Route("api/customers")]
[Produces("application/json")]
public class CustomersController : ControllerBase
{
    /// <summary>
    /// Lista clientes filtrando por tenant, empresa o sucursal.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CustomerDetail>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CustomerDetail>>> Get(
        [FromQuery] string? tenantCode,
        [FromQuery] string? companyCode,
        [FromQuery] string? branchCode,
        [FromServices] IQueryHandler<GetCustomersQuery, IReadOnlyList<CustomerDetail>> handler,
        CancellationToken cancellationToken)
    {
        var customers = await handler.Handle(new GetCustomersQuery(tenantCode, companyCode, branchCode), cancellationToken);
        return Ok(customers);
    }

    /// <summary>
    /// Detalle de cliente por identificador.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CustomerDetail), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDetail>> GetById(
        Guid id,
        [FromServices] IQueryHandler<GetCustomerByIdQuery, CustomerDetail?> handler,
        CancellationToken cancellationToken)
    {
        var customer = await handler.Handle(new GetCustomerByIdQuery(id), cancellationToken);
        return customer is null ? NotFound() : Ok(customer);
    }
}
