using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CommentsControllerBase : ControllerBase
{
    protected readonly ICommentsService _service;

    public CommentsControllerBase(ICommentsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Comment
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Comment>> CreateComment(CommentCreateInput input)
    {
        var comment = await _service.CreateComment(input);

        return CreatedAtAction(nameof(Comment), new { id = comment.Id }, comment);
    }

    /// <summary>
    /// Delete one Comment
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteComment([FromRoute()] CommentWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteComment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Comments
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Comment>>> Comments(
        [FromQuery()] CommentFindManyArgs filter
    )
    {
        return Ok(await _service.Comments(filter));
    }

    /// <summary>
    /// Meta data about Comment records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CommentsMeta(
        [FromQuery()] CommentFindManyArgs filter
    )
    {
        return Ok(await _service.CommentsMeta(filter));
    }

    /// <summary>
    /// Get one Comment
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Comment>> Comment([FromRoute()] CommentWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Comment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Comment
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateComment(
        [FromRoute()] CommentWhereUniqueInput uniqueId,
        [FromQuery()] CommentUpdateInput commentUpdateDto
    )
    {
        try
        {
            await _service.UpdateComment(uniqueId, commentUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
