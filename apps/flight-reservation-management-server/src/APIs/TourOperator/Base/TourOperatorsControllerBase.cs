using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class TourOperatorsControllerBase : ControllerBase
{
    protected readonly ITourOperatorsService _service;

    public TourOperatorsControllerBase(ITourOperatorsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Tour Operator
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<TourOperator>> CreateTourOperator(TourOperatorCreateInput input)
    {
        var tourOperator = await _service.CreateTourOperator(input);

        return CreatedAtAction(nameof(TourOperator), new { id = tourOperator.Id }, tourOperator);
    }

    /// <summary>
    /// Delete one Tour Operator
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteTourOperator(
        [FromRoute()] TourOperatorWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteTourOperator(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Tour Operators
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<TourOperator>>> TourOperators(
        [FromQuery()] TourOperatorFindManyArgs filter
    )
    {
        return Ok(await _service.TourOperators(filter));
    }

    /// <summary>
    /// Meta data about Tour Operator records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> TourOperatorsMeta(
        [FromQuery()] TourOperatorFindManyArgs filter
    )
    {
        return Ok(await _service.TourOperatorsMeta(filter));
    }

    /// <summary>
    /// Get one Tour Operator
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<TourOperator>> TourOperator(
        [FromRoute()] TourOperatorWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.TourOperator(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Tour Operator
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateTourOperator(
        [FromRoute()] TourOperatorWhereUniqueInput uniqueId,
        [FromQuery()] TourOperatorUpdateInput tourOperatorUpdateDto
    )
    {
        try
        {
            await _service.UpdateTourOperator(uniqueId, tourOperatorUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
