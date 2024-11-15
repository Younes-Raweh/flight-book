using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class VouchersControllerBase : ControllerBase
{
    protected readonly IVouchersService _service;

    public VouchersControllerBase(IVouchersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Voucher
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Voucher>> CreateVoucher(VoucherCreateInput input)
    {
        var voucher = await _service.CreateVoucher(input);

        return CreatedAtAction(nameof(Voucher), new { id = voucher.Id }, voucher);
    }

    /// <summary>
    /// Delete one Voucher
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteVoucher([FromRoute()] VoucherWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteVoucher(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Vouchers
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Voucher>>> Vouchers(
        [FromQuery()] VoucherFindManyArgs filter
    )
    {
        return Ok(await _service.Vouchers(filter));
    }

    /// <summary>
    /// Meta data about Voucher records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> VouchersMeta(
        [FromQuery()] VoucherFindManyArgs filter
    )
    {
        return Ok(await _service.VouchersMeta(filter));
    }

    /// <summary>
    /// Get one Voucher
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Voucher>> Voucher([FromRoute()] VoucherWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Voucher(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Voucher
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateVoucher(
        [FromRoute()] VoucherWhereUniqueInput uniqueId,
        [FromQuery()] VoucherUpdateInput voucherUpdateDto
    )
    {
        try
        {
            await _service.UpdateVoucher(uniqueId, voucherUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
