using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PackageModelsControllerBase : ControllerBase
{
    protected readonly IPackageModelsService _service;

    public PackageModelsControllerBase(IPackageModelsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Package
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<PackageModel>> CreatePackageModel(PackageModelCreateInput input)
    {
        var packageModel = await _service.CreatePackageModel(input);

        return CreatedAtAction(nameof(PackageModel), new { id = packageModel.Id }, packageModel);
    }

    /// <summary>
    /// Delete one Package
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeletePackageModel(
        [FromRoute()] PackageModelWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeletePackageModel(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Packages
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<PackageModel>>> PackageModels(
        [FromQuery()] PackageModelFindManyArgs filter
    )
    {
        return Ok(await _service.PackageModels(filter));
    }

    /// <summary>
    /// Meta data about Package records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PackageModelsMeta(
        [FromQuery()] PackageModelFindManyArgs filter
    )
    {
        return Ok(await _service.PackageModelsMeta(filter));
    }

    /// <summary>
    /// Get one Package
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<PackageModel>> PackageModel(
        [FromRoute()] PackageModelWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.PackageModel(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Package
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdatePackageModel(
        [FromRoute()] PackageModelWhereUniqueInput uniqueId,
        [FromQuery()] PackageModelUpdateInput packageModelUpdateDto
    )
    {
        try
        {
            await _service.UpdatePackageModel(uniqueId, packageModelUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
