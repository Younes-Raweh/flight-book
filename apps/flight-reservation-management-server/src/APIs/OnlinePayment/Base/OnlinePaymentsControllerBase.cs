using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class OnlinePaymentsControllerBase : ControllerBase
{
    protected readonly IOnlinePaymentsService _service;

    public OnlinePaymentsControllerBase(IOnlinePaymentsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one OnlinePayment
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<OnlinePayment>> CreateOnlinePayment(
        OnlinePaymentCreateInput input
    )
    {
        var onlinePayment = await _service.CreateOnlinePayment(input);

        return CreatedAtAction(nameof(OnlinePayment), new { id = onlinePayment.Id }, onlinePayment);
    }

    /// <summary>
    /// Delete one OnlinePayment
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteOnlinePayment(
        [FromRoute()] OnlinePaymentWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteOnlinePayment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many OnlinePayments
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<OnlinePayment>>> OnlinePayments(
        [FromQuery()] OnlinePaymentFindManyArgs filter
    )
    {
        return Ok(await _service.OnlinePayments(filter));
    }

    /// <summary>
    /// Meta data about OnlinePayment records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> OnlinePaymentsMeta(
        [FromQuery()] OnlinePaymentFindManyArgs filter
    )
    {
        return Ok(await _service.OnlinePaymentsMeta(filter));
    }

    /// <summary>
    /// Get one OnlinePayment
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<OnlinePayment>> OnlinePayment(
        [FromRoute()] OnlinePaymentWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.OnlinePayment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one OnlinePayment
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateOnlinePayment(
        [FromRoute()] OnlinePaymentWhereUniqueInput uniqueId,
        [FromQuery()] OnlinePaymentUpdateInput onlinePaymentUpdateDto
    )
    {
        try
        {
            await _service.UpdateOnlinePayment(uniqueId, onlinePaymentUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
