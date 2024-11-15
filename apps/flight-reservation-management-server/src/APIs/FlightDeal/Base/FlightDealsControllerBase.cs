using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class FlightDealsControllerBase : ControllerBase
{
    protected readonly IFlightDealsService _service;

    public FlightDealsControllerBase(IFlightDealsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one FlightDeal
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<FlightDeal>> CreateFlightDeal(FlightDealCreateInput input)
    {
        var flightDeal = await _service.CreateFlightDeal(input);

        return CreatedAtAction(nameof(FlightDeal), new { id = flightDeal.Id }, flightDeal);
    }

    /// <summary>
    /// Delete one FlightDeal
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteFlightDeal(
        [FromRoute()] FlightDealWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteFlightDeal(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many FlightDeals
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<FlightDeal>>> FlightDeals(
        [FromQuery()] FlightDealFindManyArgs filter
    )
    {
        return Ok(await _service.FlightDeals(filter));
    }

    /// <summary>
    /// Meta data about FlightDeal records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> FlightDealsMeta(
        [FromQuery()] FlightDealFindManyArgs filter
    )
    {
        return Ok(await _service.FlightDealsMeta(filter));
    }

    /// <summary>
    /// Get one FlightDeal
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<FlightDeal>> FlightDeal(
        [FromRoute()] FlightDealWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.FlightDeal(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one FlightDeal
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateFlightDeal(
        [FromRoute()] FlightDealWhereUniqueInput uniqueId,
        [FromQuery()] FlightDealUpdateInput flightDealUpdateDto
    )
    {
        try
        {
            await _service.UpdateFlightDeal(uniqueId, flightDealUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
