using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class HotelsControllerBase : ControllerBase
{
    protected readonly IHotelsService _service;

    public HotelsControllerBase(IHotelsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Hotel
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Hotel>> CreateHotel(HotelCreateInput input)
    {
        var hotel = await _service.CreateHotel(input);

        return CreatedAtAction(nameof(Hotel), new { id = hotel.Id }, hotel);
    }

    /// <summary>
    /// Delete one Hotel
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteHotel([FromRoute()] HotelWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteHotel(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Hotels
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Hotel>>> Hotels([FromQuery()] HotelFindManyArgs filter)
    {
        return Ok(await _service.Hotels(filter));
    }

    /// <summary>
    /// Meta data about Hotel records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> HotelsMeta([FromQuery()] HotelFindManyArgs filter)
    {
        return Ok(await _service.HotelsMeta(filter));
    }

    /// <summary>
    /// Get one Hotel
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Hotel>> Hotel([FromRoute()] HotelWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Hotel(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Hotel
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateHotel(
        [FromRoute()] HotelWhereUniqueInput uniqueId,
        [FromQuery()] HotelUpdateInput hotelUpdateDto
    )
    {
        try
        {
            await _service.UpdateHotel(uniqueId, hotelUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
