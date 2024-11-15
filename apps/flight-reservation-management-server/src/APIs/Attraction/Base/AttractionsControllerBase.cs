using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AttractionsControllerBase : ControllerBase
{
    protected readonly IAttractionsService _service;

    public AttractionsControllerBase(IAttractionsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Attraction
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Attraction>> CreateAttraction(AttractionCreateInput input)
    {
        var attraction = await _service.CreateAttraction(input);

        return CreatedAtAction(nameof(Attraction), new { id = attraction.Id }, attraction);
    }

    /// <summary>
    /// Delete one Attraction
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteAttraction(
        [FromRoute()] AttractionWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteAttraction(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Attractions
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Attraction>>> Attractions(
        [FromQuery()] AttractionFindManyArgs filter
    )
    {
        return Ok(await _service.Attractions(filter));
    }

    /// <summary>
    /// Meta data about Attraction records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AttractionsMeta(
        [FromQuery()] AttractionFindManyArgs filter
    )
    {
        return Ok(await _service.AttractionsMeta(filter));
    }

    /// <summary>
    /// Get one Attraction
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Attraction>> Attraction(
        [FromRoute()] AttractionWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Attraction(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Attraction
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateAttraction(
        [FromRoute()] AttractionWhereUniqueInput uniqueId,
        [FromQuery()] AttractionUpdateInput attractionUpdateDto
    )
    {
        try
        {
            await _service.UpdateAttraction(uniqueId, attractionUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
