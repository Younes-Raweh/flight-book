using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PayLatersControllerBase : ControllerBase
{
    protected readonly IPayLatersService _service;

    public PayLatersControllerBase(IPayLatersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one PayLater
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<PayLater>> CreatePayLater(PayLaterCreateInput input)
    {
        var payLater = await _service.CreatePayLater(input);

        return CreatedAtAction(nameof(PayLater), new { id = payLater.Id }, payLater);
    }

    /// <summary>
    /// Delete one PayLater
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeletePayLater([FromRoute()] PayLaterWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeletePayLater(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many PayLaters
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<PayLater>>> PayLaters(
        [FromQuery()] PayLaterFindManyArgs filter
    )
    {
        return Ok(await _service.PayLaters(filter));
    }

    /// <summary>
    /// Meta data about PayLater records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PayLatersMeta(
        [FromQuery()] PayLaterFindManyArgs filter
    )
    {
        return Ok(await _service.PayLatersMeta(filter));
    }

    /// <summary>
    /// Get one PayLater
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<PayLater>> PayLater(
        [FromRoute()] PayLaterWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.PayLater(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one PayLater
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdatePayLater(
        [FromRoute()] PayLaterWhereUniqueInput uniqueId,
        [FromQuery()] PayLaterUpdateInput payLaterUpdateDto
    )
    {
        try
        {
            await _service.UpdatePayLater(uniqueId, payLaterUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
