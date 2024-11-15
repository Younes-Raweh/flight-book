using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PackageTypesControllerBase : ControllerBase
{
    protected readonly IPackageTypesService _service;

    public PackageTypesControllerBase(IPackageTypesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one PackageType
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<PackageType>> CreatePackageType(PackageTypeCreateInput input)
    {
        var packageType = await _service.CreatePackageType(input);

        return CreatedAtAction(nameof(PackageType), new { id = packageType.Id }, packageType);
    }

    /// <summary>
    /// Delete one PackageType
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeletePackageType(
        [FromRoute()] PackageTypeWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeletePackageType(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many PackageTypes
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<PackageType>>> PackageTypes(
        [FromQuery()] PackageTypeFindManyArgs filter
    )
    {
        return Ok(await _service.PackageTypes(filter));
    }

    /// <summary>
    /// Meta data about PackageType records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PackageTypesMeta(
        [FromQuery()] PackageTypeFindManyArgs filter
    )
    {
        return Ok(await _service.PackageTypesMeta(filter));
    }

    /// <summary>
    /// Get one PackageType
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<PackageType>> PackageType(
        [FromRoute()] PackageTypeWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.PackageType(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one PackageType
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdatePackageType(
        [FromRoute()] PackageTypeWhereUniqueInput uniqueId,
        [FromQuery()] PackageTypeUpdateInput packageTypeUpdateDto
    )
    {
        try
        {
            await _service.UpdatePackageType(uniqueId, packageTypeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
