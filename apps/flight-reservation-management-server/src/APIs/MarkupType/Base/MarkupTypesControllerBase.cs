using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class MarkupTypesControllerBase : ControllerBase
{
    protected readonly IMarkupTypesService _service;

    public MarkupTypesControllerBase(IMarkupTypesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one MarkupType
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<MarkupType>> CreateMarkupType(MarkupTypeCreateInput input)
    {
        var markupType = await _service.CreateMarkupType(input);

        return CreatedAtAction(nameof(MarkupType), new { id = markupType.Id }, markupType);
    }

    /// <summary>
    /// Delete one MarkupType
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteMarkupType(
        [FromRoute()] MarkupTypeWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteMarkupType(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many MarkupTypes
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<MarkupType>>> MarkupTypes(
        [FromQuery()] MarkupTypeFindManyArgs filter
    )
    {
        return Ok(await _service.MarkupTypes(filter));
    }

    /// <summary>
    /// Meta data about MarkupType records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> MarkupTypesMeta(
        [FromQuery()] MarkupTypeFindManyArgs filter
    )
    {
        return Ok(await _service.MarkupTypesMeta(filter));
    }

    /// <summary>
    /// Get one MarkupType
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<MarkupType>> MarkupType(
        [FromRoute()] MarkupTypeWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.MarkupType(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one MarkupType
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateMarkupType(
        [FromRoute()] MarkupTypeWhereUniqueInput uniqueId,
        [FromQuery()] MarkupTypeUpdateInput markupTypeUpdateDto
    )
    {
        try
        {
            await _service.UpdateMarkupType(uniqueId, markupTypeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
