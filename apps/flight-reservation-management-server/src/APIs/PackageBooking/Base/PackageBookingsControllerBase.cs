using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PackageBookingsControllerBase : ControllerBase
{
    protected readonly IPackageBookingsService _service;

    public PackageBookingsControllerBase(IPackageBookingsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one PackageBooking
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<PackageBooking>> CreatePackageBooking(
        PackageBookingCreateInput input
    )
    {
        var packageBooking = await _service.CreatePackageBooking(input);

        return CreatedAtAction(
            nameof(PackageBooking),
            new { id = packageBooking.Id },
            packageBooking
        );
    }

    /// <summary>
    /// Delete one PackageBooking
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeletePackageBooking(
        [FromRoute()] PackageBookingWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeletePackageBooking(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many PackageBookings
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<PackageBooking>>> PackageBookings(
        [FromQuery()] PackageBookingFindManyArgs filter
    )
    {
        return Ok(await _service.PackageBookings(filter));
    }

    /// <summary>
    /// Meta data about PackageBooking records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PackageBookingsMeta(
        [FromQuery()] PackageBookingFindManyArgs filter
    )
    {
        return Ok(await _service.PackageBookingsMeta(filter));
    }

    /// <summary>
    /// Get one PackageBooking
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<PackageBooking>> PackageBooking(
        [FromRoute()] PackageBookingWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.PackageBooking(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one PackageBooking
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdatePackageBooking(
        [FromRoute()] PackageBookingWhereUniqueInput uniqueId,
        [FromQuery()] PackageBookingUpdateInput packageBookingUpdateDto
    )
    {
        try
        {
            await _service.UpdatePackageBooking(uniqueId, packageBookingUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
