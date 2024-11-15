using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AgencyProfilesControllerBase : ControllerBase
{
    protected readonly IAgencyProfilesService _service;

    public AgencyProfilesControllerBase(IAgencyProfilesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one AgencyProfile
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<AgencyProfile>> CreateAgencyProfile(
        AgencyProfileCreateInput input
    )
    {
        var agencyProfile = await _service.CreateAgencyProfile(input);

        return CreatedAtAction(nameof(AgencyProfile), new { id = agencyProfile.Id }, agencyProfile);
    }

    /// <summary>
    /// Delete one AgencyProfile
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteAgencyProfile(
        [FromRoute()] AgencyProfileWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteAgencyProfile(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many AgencyProfiles
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<AgencyProfile>>> AgencyProfiles(
        [FromQuery()] AgencyProfileFindManyArgs filter
    )
    {
        return Ok(await _service.AgencyProfiles(filter));
    }

    /// <summary>
    /// Meta data about AgencyProfile records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AgencyProfilesMeta(
        [FromQuery()] AgencyProfileFindManyArgs filter
    )
    {
        return Ok(await _service.AgencyProfilesMeta(filter));
    }

    /// <summary>
    /// Get one AgencyProfile
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<AgencyProfile>> AgencyProfile(
        [FromRoute()] AgencyProfileWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.AgencyProfile(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one AgencyProfile
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateAgencyProfile(
        [FromRoute()] AgencyProfileWhereUniqueInput uniqueId,
        [FromQuery()] AgencyProfileUpdateInput agencyProfileUpdateDto
    )
    {
        try
        {
            await _service.UpdateAgencyProfile(uniqueId, agencyProfileUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
