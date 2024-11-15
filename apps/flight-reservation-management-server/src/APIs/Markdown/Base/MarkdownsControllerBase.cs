using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class MarkdownsControllerBase : ControllerBase
{
    protected readonly IMarkdownsService _service;

    public MarkdownsControllerBase(IMarkdownsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Markdown
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Markdown>> CreateMarkdown(MarkdownCreateInput input)
    {
        var markdown = await _service.CreateMarkdown(input);

        return CreatedAtAction(nameof(Markdown), new { id = markdown.Id }, markdown);
    }

    /// <summary>
    /// Delete one Markdown
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteMarkdown([FromRoute()] MarkdownWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteMarkdown(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Markdowns
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Markdown>>> Markdowns(
        [FromQuery()] MarkdownFindManyArgs filter
    )
    {
        return Ok(await _service.Markdowns(filter));
    }

    /// <summary>
    /// Meta data about Markdown records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> MarkdownsMeta(
        [FromQuery()] MarkdownFindManyArgs filter
    )
    {
        return Ok(await _service.MarkdownsMeta(filter));
    }

    /// <summary>
    /// Get one Markdown
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Markdown>> Markdown(
        [FromRoute()] MarkdownWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Markdown(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Markdown
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateMarkdown(
        [FromRoute()] MarkdownWhereUniqueInput uniqueId,
        [FromQuery()] MarkdownUpdateInput markdownUpdateDto
    )
    {
        try
        {
            await _service.UpdateMarkdown(uniqueId, markdownUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
