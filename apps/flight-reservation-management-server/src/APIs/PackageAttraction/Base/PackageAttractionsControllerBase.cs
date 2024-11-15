using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PackageAttractionsControllerBase : ControllerBase
{
    protected readonly IPackageAttractionsService _service;

    public PackageAttractionsControllerBase(IPackageAttractionsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one PackageAttraction
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<PackageAttraction>> CreatePackageAttraction(
        PackageAttractionCreateInput input
    )
    {
        var packageAttraction = await _service.CreatePackageAttraction(input);

        return CreatedAtAction(
            nameof(PackageAttraction),
            new { id = packageAttraction.Id },
            packageAttraction
        );
    }

    /// <summary>
    /// Delete one PackageAttraction
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeletePackageAttraction(
        [FromRoute()] PackageAttractionWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeletePackageAttraction(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many PackageAttractions
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<PackageAttraction>>> PackageAttractions(
        [FromQuery()] PackageAttractionFindManyArgs filter
    )
    {
        return Ok(await _service.PackageAttractions(filter));
    }

    /// <summary>
    /// Meta data about PackageAttraction records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PackageAttractionsMeta(
        [FromQuery()] PackageAttractionFindManyArgs filter
    )
    {
        return Ok(await _service.PackageAttractionsMeta(filter));
    }

    /// <summary>
    /// Get one PackageAttraction
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<PackageAttraction>> PackageAttraction(
        [FromRoute()] PackageAttractionWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.PackageAttraction(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one PackageAttraction
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdatePackageAttraction(
        [FromRoute()] PackageAttractionWhereUniqueInput uniqueId,
        [FromQuery()] PackageAttractionUpdateInput packageAttractionUpdateDto
    )
    {
        try
        {
            await _service.UpdatePackageAttraction(uniqueId, packageAttractionUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
