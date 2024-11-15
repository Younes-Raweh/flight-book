using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class MarkupValueTypesControllerBase : ControllerBase
{
    protected readonly IMarkupValueTypesService _service;

    public MarkupValueTypesControllerBase(IMarkupValueTypesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one MarkupValueType
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<MarkupValueType>> CreateMarkupValueType(
        MarkupValueTypeCreateInput input
    )
    {
        var markupValueType = await _service.CreateMarkupValueType(input);

        return CreatedAtAction(
            nameof(MarkupValueType),
            new { id = markupValueType.Id },
            markupValueType
        );
    }

    /// <summary>
    /// Delete one MarkupValueType
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteMarkupValueType(
        [FromRoute()] MarkupValueTypeWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteMarkupValueType(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many MarkupValueTypes
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<MarkupValueType>>> MarkupValueTypes(
        [FromQuery()] MarkupValueTypeFindManyArgs filter
    )
    {
        return Ok(await _service.MarkupValueTypes(filter));
    }

    /// <summary>
    /// Meta data about MarkupValueType records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> MarkupValueTypesMeta(
        [FromQuery()] MarkupValueTypeFindManyArgs filter
    )
    {
        return Ok(await _service.MarkupValueTypesMeta(filter));
    }

    /// <summary>
    /// Get one MarkupValueType
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<MarkupValueType>> MarkupValueType(
        [FromRoute()] MarkupValueTypeWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.MarkupValueType(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one MarkupValueType
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateMarkupValueType(
        [FromRoute()] MarkupValueTypeWhereUniqueInput uniqueId,
        [FromQuery()] MarkupValueTypeUpdateInput markupValueTypeUpdateDto
    )
    {
        try
        {
            await _service.UpdateMarkupValueType(uniqueId, markupValueTypeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
