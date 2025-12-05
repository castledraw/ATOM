using ERP.Shared.Domain.Abstractions;
using ERP.Shared.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers;

[ApiController]
[Route("api/users")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IRepository<UserAccount> _users;

    public UsersController(IRepository<UserAccount> users)
    {
        _users = users;
    }

    /// <summary>
    /// Lista de usuarios mock para pruebas de integración.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var users = await _users.ListAsync(cancellationToken);
        return Ok(users);
    }

    /// <summary>
    /// Crea un usuario habilitado para Cognito/Identity.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(UserAccount), StatusCodes.Status201Created)]
    public async Task<ActionResult<UserAccount>> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = new UserAccount(Guid.NewGuid(), request.Email, request.TenantCode, request.Roles, true);
        await _users.AddAsync(user, cancellationToken);
        return CreatedAtAction(nameof(List), new { id = user.Id }, user);
    }

    /// <summary>
    /// Actualiza roles y habilitación de un usuario.
    /// </summary>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(typeof(UserAccount), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserAccount>> Update(Guid id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var updated = await _users.UpdateAsync(u => u.Id == id, existing => existing with
        {
            Roles = request.Roles ?? existing.Roles,
            Enabled = request.Enabled ?? existing.Enabled
        }, cancellationToken);

        return updated is null ? NotFound() : Ok(updated);
    }
}

public record CreateUserRequest(string Email, string TenantCode, string[] Roles);

public record UpdateUserRequest(string[]? Roles, bool? Enabled);
