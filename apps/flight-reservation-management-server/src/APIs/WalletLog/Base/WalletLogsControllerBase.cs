using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class WalletLogsControllerBase : ControllerBase
{
    protected readonly IWalletLogsService _service;

    public WalletLogsControllerBase(IWalletLogsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one WalletLog
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<WalletLog>> CreateWalletLog(WalletLogCreateInput input)
    {
        var walletLog = await _service.CreateWalletLog(input);

        return CreatedAtAction(nameof(WalletLog), new { id = walletLog.Id }, walletLog);
    }

    /// <summary>
    /// Delete one WalletLog
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteWalletLog(
        [FromRoute()] WalletLogWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteWalletLog(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many WalletLogs
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<WalletLog>>> WalletLogs(
        [FromQuery()] WalletLogFindManyArgs filter
    )
    {
        return Ok(await _service.WalletLogs(filter));
    }

    /// <summary>
    /// Meta data about WalletLog records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> WalletLogsMeta(
        [FromQuery()] WalletLogFindManyArgs filter
    )
    {
        return Ok(await _service.WalletLogsMeta(filter));
    }

    /// <summary>
    /// Get one WalletLog
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<WalletLog>> WalletLog(
        [FromRoute()] WalletLogWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.WalletLog(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one WalletLog
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateWalletLog(
        [FromRoute()] WalletLogWhereUniqueInput uniqueId,
        [FromQuery()] WalletLogUpdateInput walletLogUpdateDto
    )
    {
        try
        {
            await _service.UpdateWalletLog(uniqueId, walletLogUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
