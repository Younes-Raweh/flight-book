using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PackageHotelsControllerBase : ControllerBase
{
    protected readonly IPackageHotelsService _service;

    public PackageHotelsControllerBase(IPackageHotelsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one PackageHotel
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<PackageHotel>> CreatePackageHotel(PackageHotelCreateInput input)
    {
        var packageHotel = await _service.CreatePackageHotel(input);

        return CreatedAtAction(nameof(PackageHotel), new { id = packageHotel.Id }, packageHotel);
    }

    /// <summary>
    /// Delete one PackageHotel
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeletePackageHotel(
        [FromRoute()] PackageHotelWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeletePackageHotel(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many PackageHotels
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<PackageHotel>>> PackageHotels(
        [FromQuery()] PackageHotelFindManyArgs filter
    )
    {
        return Ok(await _service.PackageHotels(filter));
    }

    /// <summary>
    /// Meta data about PackageHotel records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PackageHotelsMeta(
        [FromQuery()] PackageHotelFindManyArgs filter
    )
    {
        return Ok(await _service.PackageHotelsMeta(filter));
    }

    /// <summary>
    /// Get one PackageHotel
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<PackageHotel>> PackageHotel(
        [FromRoute()] PackageHotelWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.PackageHotel(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one PackageHotel
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdatePackageHotel(
        [FromRoute()] PackageHotelWhereUniqueInput uniqueId,
        [FromQuery()] PackageHotelUpdateInput packageHotelUpdateDto
    )
    {
        try
        {
            await _service.UpdatePackageHotel(uniqueId, packageHotelUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
