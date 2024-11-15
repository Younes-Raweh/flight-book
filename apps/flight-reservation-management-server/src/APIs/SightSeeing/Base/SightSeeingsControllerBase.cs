using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class SightSeeingsControllerBase : ControllerBase
{
    protected readonly ISightSeeingsService _service;

    public SightSeeingsControllerBase(ISightSeeingsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one SightSeeing
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<SightSeeing>> CreateSightSeeing(SightSeeingCreateInput input)
    {
        var sightSeeing = await _service.CreateSightSeeing(input);

        return CreatedAtAction(nameof(SightSeeing), new { id = sightSeeing.Id }, sightSeeing);
    }

    /// <summary>
    /// Delete one SightSeeing
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteSightSeeing(
        [FromRoute()] SightSeeingWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteSightSeeing(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many SightSeeings
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<SightSeeing>>> SightSeeings(
        [FromQuery()] SightSeeingFindManyArgs filter
    )
    {
        return Ok(await _service.SightSeeings(filter));
    }

    /// <summary>
    /// Meta data about SightSeeing records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> SightSeeingsMeta(
        [FromQuery()] SightSeeingFindManyArgs filter
    )
    {
        return Ok(await _service.SightSeeingsMeta(filter));
    }

    /// <summary>
    /// Get one SightSeeing
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<SightSeeing>> SightSeeing(
        [FromRoute()] SightSeeingWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.SightSeeing(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one SightSeeing
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateSightSeeing(
        [FromRoute()] SightSeeingWhereUniqueInput uniqueId,
        [FromQuery()] SightSeeingUpdateInput sightSeeingUpdateDto
    )
    {
        try
        {
            await _service.UpdateSightSeeing(uniqueId, sightSeeingUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
