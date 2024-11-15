using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CarBookingsControllerBase : ControllerBase
{
    protected readonly ICarBookingsService _service;

    public CarBookingsControllerBase(ICarBookingsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one CarBooking
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<CarBooking>> CreateCarBooking(CarBookingCreateInput input)
    {
        var carBooking = await _service.CreateCarBooking(input);

        return CreatedAtAction(nameof(CarBooking), new { id = carBooking.Id }, carBooking);
    }

    /// <summary>
    /// Delete one CarBooking
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteCarBooking(
        [FromRoute()] CarBookingWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteCarBooking(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many CarBookings
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<CarBooking>>> CarBookings(
        [FromQuery()] CarBookingFindManyArgs filter
    )
    {
        return Ok(await _service.CarBookings(filter));
    }

    /// <summary>
    /// Meta data about CarBooking records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CarBookingsMeta(
        [FromQuery()] CarBookingFindManyArgs filter
    )
    {
        return Ok(await _service.CarBookingsMeta(filter));
    }

    /// <summary>
    /// Get one CarBooking
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<CarBooking>> CarBooking(
        [FromRoute()] CarBookingWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.CarBooking(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one CarBooking
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateCarBooking(
        [FromRoute()] CarBookingWhereUniqueInput uniqueId,
        [FromQuery()] CarBookingUpdateInput carBookingUpdateDto
    )
    {
        try
        {
            await _service.UpdateCarBooking(uniqueId, carBookingUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
