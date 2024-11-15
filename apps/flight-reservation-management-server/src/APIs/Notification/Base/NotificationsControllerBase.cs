using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class NotificationsControllerBase : ControllerBase
{
    protected readonly INotificationsService _service;

    public NotificationsControllerBase(INotificationsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Notification
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Notification>> CreateNotification(NotificationCreateInput input)
    {
        var notification = await _service.CreateNotification(input);

        return CreatedAtAction(nameof(Notification), new { id = notification.Id }, notification);
    }

    /// <summary>
    /// Delete one Notification
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteNotification(
        [FromRoute()] NotificationWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteNotification(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Notifications
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Notification>>> Notifications(
        [FromQuery()] NotificationFindManyArgs filter
    )
    {
        return Ok(await _service.Notifications(filter));
    }

    /// <summary>
    /// Meta data about Notification records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> NotificationsMeta(
        [FromQuery()] NotificationFindManyArgs filter
    )
    {
        return Ok(await _service.NotificationsMeta(filter));
    }

    /// <summary>
    /// Get one Notification
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Notification>> Notification(
        [FromRoute()] NotificationWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Notification(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Notification
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateNotification(
        [FromRoute()] NotificationWhereUniqueInput uniqueId,
        [FromQuery()] NotificationUpdateInput notificationUpdateDto
    )
    {
        try
        {
            await _service.UpdateNotification(uniqueId, notificationUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
