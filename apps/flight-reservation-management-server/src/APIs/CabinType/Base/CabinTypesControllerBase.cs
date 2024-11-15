using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CabinTypesControllerBase : ControllerBase
{
    protected readonly ICabinTypesService _service;

    public CabinTypesControllerBase(ICabinTypesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one CabinType
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<CabinType>> CreateCabinType(CabinTypeCreateInput input)
    {
        var cabinType = await _service.CreateCabinType(input);

        return CreatedAtAction(nameof(CabinType), new { id = cabinType.Id }, cabinType);
    }

    /// <summary>
    /// Delete one CabinType
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteCabinType(
        [FromRoute()] CabinTypeWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteCabinType(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many CabinTypes
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<CabinType>>> CabinTypes(
        [FromQuery()] CabinTypeFindManyArgs filter
    )
    {
        return Ok(await _service.CabinTypes(filter));
    }

    /// <summary>
    /// Meta data about CabinType records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CabinTypesMeta(
        [FromQuery()] CabinTypeFindManyArgs filter
    )
    {
        return Ok(await _service.CabinTypesMeta(filter));
    }

    /// <summary>
    /// Get one CabinType
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<CabinType>> CabinType(
        [FromRoute()] CabinTypeWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.CabinType(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one CabinType
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateCabinType(
        [FromRoute()] CabinTypeWhereUniqueInput uniqueId,
        [FromQuery()] CabinTypeUpdateInput cabinTypeUpdateDto
    )
    {
        try
        {
            await _service.UpdateCabinType(uniqueId, cabinTypeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
