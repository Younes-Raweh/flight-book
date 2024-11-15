using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class BanksControllerBase : ControllerBase
{
    protected readonly IBanksService _service;

    public BanksControllerBase(IBanksService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Bank
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Bank>> CreateBank(BankCreateInput input)
    {
        var bank = await _service.CreateBank(input);

        return CreatedAtAction(nameof(Bank), new { id = bank.Id }, bank);
    }

    /// <summary>
    /// Delete one Bank
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteBank([FromRoute()] BankWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteBank(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Banks
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Bank>>> Banks([FromQuery()] BankFindManyArgs filter)
    {
        return Ok(await _service.Banks(filter));
    }

    /// <summary>
    /// Meta data about Bank records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> BanksMeta([FromQuery()] BankFindManyArgs filter)
    {
        return Ok(await _service.BanksMeta(filter));
    }

    /// <summary>
    /// Get one Bank
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Bank>> Bank([FromRoute()] BankWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Bank(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Bank
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateBank(
        [FromRoute()] BankWhereUniqueInput uniqueId,
        [FromQuery()] BankUpdateInput bankUpdateDto
    )
    {
        try
        {
            await _service.UpdateBank(uniqueId, bankUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
