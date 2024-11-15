using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EmailSubscribersControllerBase : ControllerBase
{
    protected readonly IEmailSubscribersService _service;

    public EmailSubscribersControllerBase(IEmailSubscribersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one EmailSubscriber
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<EmailSubscriber>> CreateEmailSubscriber(
        EmailSubscriberCreateInput input
    )
    {
        var emailSubscriber = await _service.CreateEmailSubscriber(input);

        return CreatedAtAction(
            nameof(EmailSubscriber),
            new { id = emailSubscriber.Id },
            emailSubscriber
        );
    }

    /// <summary>
    /// Delete one EmailSubscriber
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteEmailSubscriber(
        [FromRoute()] EmailSubscriberWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteEmailSubscriber(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many EmailSubscribers
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<EmailSubscriber>>> EmailSubscribers(
        [FromQuery()] EmailSubscriberFindManyArgs filter
    )
    {
        return Ok(await _service.EmailSubscribers(filter));
    }

    /// <summary>
    /// Meta data about EmailSubscriber records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> EmailSubscribersMeta(
        [FromQuery()] EmailSubscriberFindManyArgs filter
    )
    {
        return Ok(await _service.EmailSubscribersMeta(filter));
    }

    /// <summary>
    /// Get one EmailSubscriber
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<EmailSubscriber>> EmailSubscriber(
        [FromRoute()] EmailSubscriberWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.EmailSubscriber(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one EmailSubscriber
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateEmailSubscriber(
        [FromRoute()] EmailSubscriberWhereUniqueInput uniqueId,
        [FromQuery()] EmailSubscriberUpdateInput emailSubscriberUpdateDto
    )
    {
        try
        {
            await _service.UpdateEmailSubscriber(uniqueId, emailSubscriberUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
