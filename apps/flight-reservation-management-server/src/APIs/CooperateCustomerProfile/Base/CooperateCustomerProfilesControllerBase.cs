using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CooperateCustomerProfilesControllerBase : ControllerBase
{
    protected readonly ICooperateCustomerProfilesService _service;

    public CooperateCustomerProfilesControllerBase(ICooperateCustomerProfilesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one CooperateCustomerProfile
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<CooperateCustomerProfile>> CreateCooperateCustomerProfile(
        CooperateCustomerProfileCreateInput input
    )
    {
        var cooperateCustomerProfile = await _service.CreateCooperateCustomerProfile(input);

        return CreatedAtAction(
            nameof(CooperateCustomerProfile),
            new { id = cooperateCustomerProfile.Id },
            cooperateCustomerProfile
        );
    }

    /// <summary>
    /// Delete one CooperateCustomerProfile
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteCooperateCustomerProfile(
        [FromRoute()] CooperateCustomerProfileWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteCooperateCustomerProfile(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many CooperateCustomerProfiles
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<CooperateCustomerProfile>>> CooperateCustomerProfiles(
        [FromQuery()] CooperateCustomerProfileFindManyArgs filter
    )
    {
        return Ok(await _service.CooperateCustomerProfiles(filter));
    }

    /// <summary>
    /// Meta data about CooperateCustomerProfile records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CooperateCustomerProfilesMeta(
        [FromQuery()] CooperateCustomerProfileFindManyArgs filter
    )
    {
        return Ok(await _service.CooperateCustomerProfilesMeta(filter));
    }

    /// <summary>
    /// Get one CooperateCustomerProfile
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<CooperateCustomerProfile>> CooperateCustomerProfile(
        [FromRoute()] CooperateCustomerProfileWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.CooperateCustomerProfile(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one CooperateCustomerProfile
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateCooperateCustomerProfile(
        [FromRoute()] CooperateCustomerProfileWhereUniqueInput uniqueId,
        [FromQuery()] CooperateCustomerProfileUpdateInput cooperateCustomerProfileUpdateDto
    )
    {
        try
        {
            await _service.UpdateCooperateCustomerProfile(
                uniqueId,
                cooperateCustomerProfileUpdateDto
            );
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
