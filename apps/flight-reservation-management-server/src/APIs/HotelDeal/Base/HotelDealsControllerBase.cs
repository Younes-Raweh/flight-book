using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class HotelDealsControllerBase : ControllerBase
{
    protected readonly IHotelDealsService _service;

    public HotelDealsControllerBase(IHotelDealsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one HotelDeal
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<HotelDeal>> CreateHotelDeal(HotelDealCreateInput input)
    {
        var hotelDeal = await _service.CreateHotelDeal(input);

        return CreatedAtAction(nameof(HotelDeal), new { id = hotelDeal.Id }, hotelDeal);
    }

    /// <summary>
    /// Delete one HotelDeal
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteHotelDeal(
        [FromRoute()] HotelDealWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteHotelDeal(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many HotelDeals
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<HotelDeal>>> HotelDeals(
        [FromQuery()] HotelDealFindManyArgs filter
    )
    {
        return Ok(await _service.HotelDeals(filter));
    }

    /// <summary>
    /// Meta data about HotelDeal records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> HotelDealsMeta(
        [FromQuery()] HotelDealFindManyArgs filter
    )
    {
        return Ok(await _service.HotelDealsMeta(filter));
    }

    /// <summary>
    /// Get one HotelDeal
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<HotelDeal>> HotelDeal(
        [FromRoute()] HotelDealWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.HotelDeal(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one HotelDeal
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateHotelDeal(
        [FromRoute()] HotelDealWhereUniqueInput uniqueId,
        [FromQuery()] HotelDealUpdateInput hotelDealUpdateDto
    )
    {
        try
        {
            await _service.UpdateHotelDeal(uniqueId, hotelDealUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
