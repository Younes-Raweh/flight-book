using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class MarkupsControllerBase : ControllerBase
{
    protected readonly IMarkupsService _service;

    public MarkupsControllerBase(IMarkupsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Markup
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Markup>> CreateMarkup(MarkupCreateInput input)
    {
        var markup = await _service.CreateMarkup(input);

        return CreatedAtAction(nameof(Markup), new { id = markup.Id }, markup);
    }

    /// <summary>
    /// Delete one Markup
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteMarkup([FromRoute()] MarkupWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteMarkup(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Markups
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Markup>>> Markups([FromQuery()] MarkupFindManyArgs filter)
    {
        return Ok(await _service.Markups(filter));
    }

    /// <summary>
    /// Meta data about Markup records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> MarkupsMeta(
        [FromQuery()] MarkupFindManyArgs filter
    )
    {
        return Ok(await _service.MarkupsMeta(filter));
    }

    /// <summary>
    /// Get one Markup
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Markup>> Markup([FromRoute()] MarkupWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Markup(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Markup
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateMarkup(
        [FromRoute()] MarkupWhereUniqueInput uniqueId,
        [FromQuery()] MarkupUpdateInput markupUpdateDto
    )
    {
        try
        {
            await _service.UpdateMarkup(uniqueId, markupUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
