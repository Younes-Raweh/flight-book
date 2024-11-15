using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class VatsControllerBase : ControllerBase
{
    protected readonly IVatsService _service;

    public VatsControllerBase(IVatsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Vat
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Vat>> CreateVat(VatCreateInput input)
    {
        var vat = await _service.CreateVat(input);

        return CreatedAtAction(nameof(Vat), new { id = vat.Id }, vat);
    }

    /// <summary>
    /// Delete one Vat
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteVat([FromRoute()] VatWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteVat(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Vats
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Vat>>> Vats([FromQuery()] VatFindManyArgs filter)
    {
        return Ok(await _service.Vats(filter));
    }

    /// <summary>
    /// Meta data about Vat records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> VatsMeta([FromQuery()] VatFindManyArgs filter)
    {
        return Ok(await _service.VatsMeta(filter));
    }

    /// <summary>
    /// Get one Vat
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Vat>> Vat([FromRoute()] VatWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Vat(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Vat
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateVat(
        [FromRoute()] VatWhereUniqueInput uniqueId,
        [FromQuery()] VatUpdateInput vatUpdateDto
    )
    {
        try
        {
            await _service.UpdateVat(uniqueId, vatUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
