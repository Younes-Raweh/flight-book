using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class FlightBookingsControllerBase : ControllerBase
{
    protected readonly IFlightBookingsService _service;

    public FlightBookingsControllerBase(IFlightBookingsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one FlightBooking
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<FlightBooking>> CreateFlightBooking(
        FlightBookingCreateInput input
    )
    {
        var flightBooking = await _service.CreateFlightBooking(input);

        return CreatedAtAction(nameof(FlightBooking), new { id = flightBooking.Id }, flightBooking);
    }

    /// <summary>
    /// Delete one FlightBooking
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteFlightBooking(
        [FromRoute()] FlightBookingWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteFlightBooking(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many FlightBookings
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<FlightBooking>>> FlightBookings(
        [FromQuery()] FlightBookingFindManyArgs filter
    )
    {
        return Ok(await _service.FlightBookings(filter));
    }

    /// <summary>
    /// Meta data about FlightBooking records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> FlightBookingsMeta(
        [FromQuery()] FlightBookingFindManyArgs filter
    )
    {
        return Ok(await _service.FlightBookingsMeta(filter));
    }

    /// <summary>
    /// Get one FlightBooking
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<FlightBooking>> FlightBooking(
        [FromRoute()] FlightBookingWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.FlightBooking(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one FlightBooking
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateFlightBooking(
        [FromRoute()] FlightBookingWhereUniqueInput uniqueId,
        [FromQuery()] FlightBookingUpdateInput flightBookingUpdateDto
    )
    {
        try
        {
            await _service.UpdateFlightBooking(uniqueId, flightBookingUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
