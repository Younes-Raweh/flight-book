using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class RolesControllerBase : ControllerBase
{
    protected readonly IRolesService _service;

    public RolesControllerBase(IRolesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Role
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Role>> CreateRole(RoleCreateInput input)
    {
        var role = await _service.CreateRole(input);

        return CreatedAtAction(nameof(Role), new { id = role.Id }, role);
    }

    /// <summary>
    /// Delete one Role
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteRole([FromRoute()] RoleWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteRole(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Roles
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Role>>> Roles([FromQuery()] RoleFindManyArgs filter)
    {
        return Ok(await _service.Roles(filter));
    }

    /// <summary>
    /// Meta data about Role records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> RolesMeta([FromQuery()] RoleFindManyArgs filter)
    {
        return Ok(await _service.RolesMeta(filter));
    }

    /// <summary>
    /// Get one Role
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Role>> Role([FromRoute()] RoleWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Role(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Role
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateRole(
        [FromRoute()] RoleWhereUniqueInput uniqueId,
        [FromQuery()] RoleUpdateInput roleUpdateDto
    )
    {
        try
        {
            await _service.UpdateRole(uniqueId, roleUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a role_ record for Role
    /// </summary>
    [HttpGet("{Id}/role")]
    public async Task<ActionResult<List<Role>>> GetRole([FromRoute()] RoleWhereUniqueInput uniqueId)
    {
        var role = await _service.GetRole(uniqueId);
        return Ok(role);
    }

    /// <summary>
    /// Connect multiple Roles records to Role
    /// </summary>
    [HttpPost("{Id}/roles")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> ConnectRoles(
        [FromRoute()] RoleWhereUniqueInput uniqueId,
        [FromQuery()] RoleWhereUniqueInput[] rolesId
    )
    {
        try
        {
            await _service.ConnectRoles(uniqueId, rolesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Roles records from Role
    /// </summary>
    [HttpDelete("{Id}/roles")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DisconnectRoles(
        [FromRoute()] RoleWhereUniqueInput uniqueId,
        [FromBody()] RoleWhereUniqueInput[] rolesId
    )
    {
        try
        {
            await _service.DisconnectRoles(uniqueId, rolesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Roles records for Role
    /// </summary>
    [HttpGet("{Id}/roles")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Role>>> FindRoles(
        [FromRoute()] RoleWhereUniqueInput uniqueId,
        [FromQuery()] RoleFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindRoles(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Roles records for Role
    /// </summary>
    [HttpPatch("{Id}/roles")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateRoles(
        [FromRoute()] RoleWhereUniqueInput uniqueId,
        [FromBody()] RoleWhereUniqueInput[] rolesId
    )
    {
        try
        {
            await _service.UpdateRoles(uniqueId, rolesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
