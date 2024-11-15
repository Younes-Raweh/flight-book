using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PackageCategoriesControllerBase : ControllerBase
{
    protected readonly IPackageCategoriesService _service;

    public PackageCategoriesControllerBase(IPackageCategoriesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one PackageCategory
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<PackageCategory>> CreatePackageCategory(
        PackageCategoryCreateInput input
    )
    {
        var packageCategory = await _service.CreatePackageCategory(input);

        return CreatedAtAction(
            nameof(PackageCategory),
            new { id = packageCategory.Id },
            packageCategory
        );
    }

    /// <summary>
    /// Delete one PackageCategory
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeletePackageCategory(
        [FromRoute()] PackageCategoryWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeletePackageCategory(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many PackageCategories
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<PackageCategory>>> PackageCategories(
        [FromQuery()] PackageCategoryFindManyArgs filter
    )
    {
        return Ok(await _service.PackageCategories(filter));
    }

    /// <summary>
    /// Meta data about PackageCategory records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PackageCategoriesMeta(
        [FromQuery()] PackageCategoryFindManyArgs filter
    )
    {
        return Ok(await _service.PackageCategoriesMeta(filter));
    }

    /// <summary>
    /// Get one PackageCategory
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<PackageCategory>> PackageCategory(
        [FromRoute()] PackageCategoryWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.PackageCategory(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one PackageCategory
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdatePackageCategory(
        [FromRoute()] PackageCategoryWhereUniqueInput uniqueId,
        [FromQuery()] PackageCategoryUpdateInput packageCategoryUpdateDto
    )
    {
        try
        {
            await _service.UpdatePackageCategory(uniqueId, packageCategoryUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
