using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class HotelBookingsControllerBase : ControllerBase
{
    protected readonly IHotelBookingsService _service;

    public HotelBookingsControllerBase(IHotelBookingsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one HotelBooking
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<HotelBooking>> CreateHotelBooking(HotelBookingCreateInput input)
    {
        var hotelBooking = await _service.CreateHotelBooking(input);

        return CreatedAtAction(nameof(HotelBooking), new { id = hotelBooking.Id }, hotelBooking);
    }

    /// <summary>
    /// Delete one HotelBooking
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteHotelBooking(
        [FromRoute()] HotelBookingWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteHotelBooking(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many HotelBookings
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<HotelBooking>>> HotelBookings(
        [FromQuery()] HotelBookingFindManyArgs filter
    )
    {
        return Ok(await _service.HotelBookings(filter));
    }

    /// <summary>
    /// Meta data about HotelBooking records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> HotelBookingsMeta(
        [FromQuery()] HotelBookingFindManyArgs filter
    )
    {
        return Ok(await _service.HotelBookingsMeta(filter));
    }

    /// <summary>
    /// Get one HotelBooking
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<HotelBooking>> HotelBooking(
        [FromRoute()] HotelBookingWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.HotelBooking(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one HotelBooking
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateHotelBooking(
        [FromRoute()] HotelBookingWhereUniqueInput uniqueId,
        [FromQuery()] HotelBookingUpdateInput hotelBookingUpdateDto
    )
    {
        try
        {
            await _service.UpdateHotelBooking(uniqueId, hotelBookingUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
