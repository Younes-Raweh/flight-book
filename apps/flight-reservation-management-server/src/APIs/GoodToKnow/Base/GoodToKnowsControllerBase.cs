using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class GoodToKnowsControllerBase : ControllerBase
{
    protected readonly IGoodToKnowsService _service;

    public GoodToKnowsControllerBase(IGoodToKnowsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one GoodToKnow
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<GoodToKnow>> CreateGoodToKnow(GoodToKnowCreateInput input)
    {
        var goodToKnow = await _service.CreateGoodToKnow(input);

        return CreatedAtAction(nameof(GoodToKnow), new { id = goodToKnow.Id }, goodToKnow);
    }

    /// <summary>
    /// Delete one GoodToKnow
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteGoodToKnow(
        [FromRoute()] GoodToKnowWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteGoodToKnow(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many GoodToKnows
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<GoodToKnow>>> GoodToKnows(
        [FromQuery()] GoodToKnowFindManyArgs filter
    )
    {
        return Ok(await _service.GoodToKnows(filter));
    }

    /// <summary>
    /// Meta data about GoodToKnow records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> GoodToKnowsMeta(
        [FromQuery()] GoodToKnowFindManyArgs filter
    )
    {
        return Ok(await _service.GoodToKnowsMeta(filter));
    }

    /// <summary>
    /// Get one GoodToKnow
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<GoodToKnow>> GoodToKnow(
        [FromRoute()] GoodToKnowWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.GoodToKnow(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one GoodToKnow
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateGoodToKnow(
        [FromRoute()] GoodToKnowWhereUniqueInput uniqueId,
        [FromQuery()] GoodToKnowUpdateInput goodToKnowUpdateDto
    )
    {
        try
        {
            await _service.UpdateGoodToKnow(uniqueId, goodToKnowUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
