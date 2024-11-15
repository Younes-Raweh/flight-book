using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class WalletLogTypesControllerBase : ControllerBase
{
    protected readonly IWalletLogTypesService _service;

    public WalletLogTypesControllerBase(IWalletLogTypesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one WalletLogType
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<WalletLogType>> CreateWalletLogType(
        WalletLogTypeCreateInput input
    )
    {
        var walletLogType = await _service.CreateWalletLogType(input);

        return CreatedAtAction(nameof(WalletLogType), new { id = walletLogType.Id }, walletLogType);
    }

    /// <summary>
    /// Delete one WalletLogType
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteWalletLogType(
        [FromRoute()] WalletLogTypeWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteWalletLogType(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many WalletLogTypes
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<WalletLogType>>> WalletLogTypes(
        [FromQuery()] WalletLogTypeFindManyArgs filter
    )
    {
        return Ok(await _service.WalletLogTypes(filter));
    }

    /// <summary>
    /// Meta data about WalletLogType records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> WalletLogTypesMeta(
        [FromQuery()] WalletLogTypeFindManyArgs filter
    )
    {
        return Ok(await _service.WalletLogTypesMeta(filter));
    }

    /// <summary>
    /// Get one WalletLogType
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<WalletLogType>> WalletLogType(
        [FromRoute()] WalletLogTypeWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.WalletLogType(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one WalletLogType
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateWalletLogType(
        [FromRoute()] WalletLogTypeWhereUniqueInput uniqueId,
        [FromQuery()] WalletLogTypeUpdateInput walletLogTypeUpdateDto
    )
    {
        try
        {
            await _service.UpdateWalletLogType(uniqueId, walletLogTypeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
