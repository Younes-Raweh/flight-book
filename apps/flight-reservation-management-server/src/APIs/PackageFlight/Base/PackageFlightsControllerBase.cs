using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PackageFlightsControllerBase : ControllerBase
{
    protected readonly IPackageFlightsService _service;

    public PackageFlightsControllerBase(IPackageFlightsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one PackageFlight
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<PackageFlight>> CreatePackageFlight(
        PackageFlightCreateInput input
    )
    {
        var packageFlight = await _service.CreatePackageFlight(input);

        return CreatedAtAction(nameof(PackageFlight), new { id = packageFlight.Id }, packageFlight);
    }

    /// <summary>
    /// Delete one PackageFlight
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeletePackageFlight(
        [FromRoute()] PackageFlightWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeletePackageFlight(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many PackageFlights
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<PackageFlight>>> PackageFlights(
        [FromQuery()] PackageFlightFindManyArgs filter
    )
    {
        return Ok(await _service.PackageFlights(filter));
    }

    /// <summary>
    /// Meta data about PackageFlight records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PackageFlightsMeta(
        [FromQuery()] PackageFlightFindManyArgs filter
    )
    {
        return Ok(await _service.PackageFlightsMeta(filter));
    }

    /// <summary>
    /// Get one PackageFlight
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<PackageFlight>> PackageFlight(
        [FromRoute()] PackageFlightWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.PackageFlight(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one PackageFlight
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdatePackageFlight(
        [FromRoute()] PackageFlightWhereUniqueInput uniqueId,
        [FromQuery()] PackageFlightUpdateInput packageFlightUpdateDto
    )
    {
        try
        {
            await _service.UpdatePackageFlight(uniqueId, packageFlightUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
