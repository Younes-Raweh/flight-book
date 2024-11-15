using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class BankDetailsControllerBase : ControllerBase
{
    protected readonly IBankDetailsService _service;

    public BankDetailsControllerBase(IBankDetailsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one BankDetail
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<BankDetail>> CreateBankDetail(BankDetailCreateInput input)
    {
        var bankDetail = await _service.CreateBankDetail(input);

        return CreatedAtAction(nameof(BankDetail), new { id = bankDetail.Id }, bankDetail);
    }

    /// <summary>
    /// Delete one BankDetail
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteBankDetail(
        [FromRoute()] BankDetailWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteBankDetail(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many BankDetails
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<BankDetail>>> BankDetails(
        [FromQuery()] BankDetailFindManyArgs filter
    )
    {
        return Ok(await _service.BankDetails(filter));
    }

    /// <summary>
    /// Meta data about BankDetail records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> BankDetailsMeta(
        [FromQuery()] BankDetailFindManyArgs filter
    )
    {
        return Ok(await _service.BankDetailsMeta(filter));
    }

    /// <summary>
    /// Get one BankDetail
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<BankDetail>> BankDetail(
        [FromRoute()] BankDetailWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.BankDetail(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one BankDetail
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateBankDetail(
        [FromRoute()] BankDetailWhereUniqueInput uniqueId,
        [FromQuery()] BankDetailUpdateInput bankDetailUpdateDto
    )
    {
        try
        {
            await _service.UpdateBankDetail(uniqueId, bankDetailUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
