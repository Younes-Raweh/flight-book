using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class WalletsControllerBase : ControllerBase
{
    protected readonly IWalletsService _service;

    public WalletsControllerBase(IWalletsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Wallet
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Wallet>> CreateWallet(WalletCreateInput input)
    {
        var wallet = await _service.CreateWallet(input);

        return CreatedAtAction(nameof(Wallet), new { id = wallet.Id }, wallet);
    }

    /// <summary>
    /// Delete one Wallet
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteWallet([FromRoute()] WalletWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteWallet(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Wallets
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Wallet>>> Wallets([FromQuery()] WalletFindManyArgs filter)
    {
        return Ok(await _service.Wallets(filter));
    }

    /// <summary>
    /// Meta data about Wallet records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> WalletsMeta(
        [FromQuery()] WalletFindManyArgs filter
    )
    {
        return Ok(await _service.WalletsMeta(filter));
    }

    /// <summary>
    /// Get one Wallet
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Wallet>> Wallet([FromRoute()] WalletWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Wallet(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Wallet
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateWallet(
        [FromRoute()] WalletWhereUniqueInput uniqueId,
        [FromQuery()] WalletUpdateInput walletUpdateDto
    )
    {
        try
        {
            await _service.UpdateWallet(uniqueId, walletUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
