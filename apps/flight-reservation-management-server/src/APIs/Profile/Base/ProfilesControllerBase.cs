using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ProfilesControllerBase : ControllerBase
{
    protected readonly IProfilesService _service;

    public ProfilesControllerBase(IProfilesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Profile
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Profile>> CreateProfile(ProfileCreateInput input)
    {
        var profile = await _service.CreateProfile(input);

        return CreatedAtAction(nameof(Profile), new { id = profile.Id }, profile);
    }

    /// <summary>
    /// Delete one Profile
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteProfile([FromRoute()] ProfileWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteProfile(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Profiles
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Profile>>> Profiles(
        [FromQuery()] ProfileFindManyArgs filter
    )
    {
        return Ok(await _service.Profiles(filter));
    }

    /// <summary>
    /// Meta data about Profile records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ProfilesMeta(
        [FromQuery()] ProfileFindManyArgs filter
    )
    {
        return Ok(await _service.ProfilesMeta(filter));
    }

    /// <summary>
    /// Get one Profile
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Profile>> Profile([FromRoute()] ProfileWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Profile(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Profile
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateProfile(
        [FromRoute()] ProfileWhereUniqueInput uniqueId,
        [FromQuery()] ProfileUpdateInput profileUpdateDto
    )
    {
        try
        {
            await _service.UpdateProfile(uniqueId, profileUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
