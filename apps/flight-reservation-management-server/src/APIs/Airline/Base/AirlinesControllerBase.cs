using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AirlinesControllerBase : ControllerBase
{
    protected readonly IAirlinesService _service;

    public AirlinesControllerBase(IAirlinesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Airline
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Airline>> CreateAirline(AirlineCreateInput input)
    {
        var airline = await _service.CreateAirline(input);

        return CreatedAtAction(nameof(Airline), new { id = airline.Id }, airline);
    }

    /// <summary>
    /// Delete one Airline
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteAirline([FromRoute()] AirlineWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteAirline(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Airlines
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Airline>>> Airlines(
        [FromQuery()] AirlineFindManyArgs filter
    )
    {
        return Ok(await _service.Airlines(filter));
    }

    /// <summary>
    /// Meta data about Airline records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AirlinesMeta(
        [FromQuery()] AirlineFindManyArgs filter
    )
    {
        return Ok(await _service.AirlinesMeta(filter));
    }

    /// <summary>
    /// Get one Airline
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Airline>> Airline([FromRoute()] AirlineWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Airline(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Airline
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateAirline(
        [FromRoute()] AirlineWhereUniqueInput uniqueId,
        [FromQuery()] AirlineUpdateInput airlineUpdateDto
    )
    {
        try
        {
            await _service.UpdateAirline(uniqueId, airlineUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
