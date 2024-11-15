using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AirportsControllerBase : ControllerBase
{
    protected readonly IAirportsService _service;

    public AirportsControllerBase(IAirportsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Airport
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Airport>> CreateAirport(AirportCreateInput input)
    {
        var airport = await _service.CreateAirport(input);

        return CreatedAtAction(nameof(Airport), new { id = airport.Id }, airport);
    }

    /// <summary>
    /// Delete one Airport
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteAirport([FromRoute()] AirportWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteAirport(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Airports
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Airport>>> Airports(
        [FromQuery()] AirportFindManyArgs filter
    )
    {
        return Ok(await _service.Airports(filter));
    }

    /// <summary>
    /// Meta data about Airport records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AirportsMeta(
        [FromQuery()] AirportFindManyArgs filter
    )
    {
        return Ok(await _service.AirportsMeta(filter));
    }

    /// <summary>
    /// Get one Airport
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Airport>> Airport([FromRoute()] AirportWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Airport(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Airport
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateAirport(
        [FromRoute()] AirportWhereUniqueInput uniqueId,
        [FromQuery()] AirportUpdateInput airportUpdateDto
    )
    {
        try
        {
            await _service.UpdateAirport(uniqueId, airportUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
