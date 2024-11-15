using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class TitlesControllerBase : ControllerBase
{
    protected readonly ITitlesService _service;

    public TitlesControllerBase(ITitlesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Title
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Title>> CreateTitle(TitleCreateInput input)
    {
        var title = await _service.CreateTitle(input);

        return CreatedAtAction(nameof(Title), new { id = title.Id }, title);
    }

    /// <summary>
    /// Delete one Title
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteTitle([FromRoute()] TitleWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteTitle(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Titles
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Title>>> Titles([FromQuery()] TitleFindManyArgs filter)
    {
        return Ok(await _service.Titles(filter));
    }

    /// <summary>
    /// Meta data about Title records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> TitlesMeta([FromQuery()] TitleFindManyArgs filter)
    {
        return Ok(await _service.TitlesMeta(filter));
    }

    /// <summary>
    /// Get one Title
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Title>> Title([FromRoute()] TitleWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Title(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Title
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateTitle(
        [FromRoute()] TitleWhereUniqueInput uniqueId,
        [FromQuery()] TitleUpdateInput titleUpdateDto
    )
    {
        try
        {
            await _service.UpdateTitle(uniqueId, titleUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
