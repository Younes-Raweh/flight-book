using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class TravelPackagesControllerBase : ControllerBase
{
    protected readonly ITravelPackagesService _service;

    public TravelPackagesControllerBase(ITravelPackagesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one TravelPackage
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<TravelPackage>> CreateTravelPackage(
        TravelPackageCreateInput input
    )
    {
        var travelPackage = await _service.CreateTravelPackage(input);

        return CreatedAtAction(nameof(TravelPackage), new { id = travelPackage.Id }, travelPackage);
    }

    /// <summary>
    /// Delete one TravelPackage
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteTravelPackage(
        [FromRoute()] TravelPackageWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteTravelPackage(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many TravelPackages
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<TravelPackage>>> TravelPackages(
        [FromQuery()] TravelPackageFindManyArgs filter
    )
    {
        return Ok(await _service.TravelPackages(filter));
    }

    /// <summary>
    /// Meta data about TravelPackage records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> TravelPackagesMeta(
        [FromQuery()] TravelPackageFindManyArgs filter
    )
    {
        return Ok(await _service.TravelPackagesMeta(filter));
    }

    /// <summary>
    /// Get one TravelPackage
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<TravelPackage>> TravelPackage(
        [FromRoute()] TravelPackageWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.TravelPackage(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one TravelPackage
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateTravelPackage(
        [FromRoute()] TravelPackageWhereUniqueInput uniqueId,
        [FromQuery()] TravelPackageUpdateInput travelPackageUpdateDto
    )
    {
        try
        {
            await _service.UpdateTravelPackage(uniqueId, travelPackageUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
