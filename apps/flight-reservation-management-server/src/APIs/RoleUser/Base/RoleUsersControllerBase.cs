using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class RoleUsersControllerBase : ControllerBase
{
    protected readonly IRoleUsersService _service;

    public RoleUsersControllerBase(IRoleUsersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one RoleUser
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<RoleUser>> CreateRoleUser(RoleUserCreateInput input)
    {
        var roleUser = await _service.CreateRoleUser(input);

        return CreatedAtAction(nameof(RoleUser), new { id = roleUser.Id }, roleUser);
    }

    /// <summary>
    /// Delete one RoleUser
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteRoleUser([FromRoute()] RoleUserWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteRoleUser(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many RoleUsers
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<RoleUser>>> RoleUsers(
        [FromQuery()] RoleUserFindManyArgs filter
    )
    {
        return Ok(await _service.RoleUsers(filter));
    }

    /// <summary>
    /// Meta data about RoleUser records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> RoleUsersMeta(
        [FromQuery()] RoleUserFindManyArgs filter
    )
    {
        return Ok(await _service.RoleUsersMeta(filter));
    }

    /// <summary>
    /// Get one RoleUser
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<RoleUser>> RoleUser(
        [FromRoute()] RoleUserWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.RoleUser(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one RoleUser
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateRoleUser(
        [FromRoute()] RoleUserWhereUniqueInput uniqueId,
        [FromQuery()] RoleUserUpdateInput roleUserUpdateDto
    )
    {
        try
        {
            await _service.UpdateRoleUser(uniqueId, roleUserUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
