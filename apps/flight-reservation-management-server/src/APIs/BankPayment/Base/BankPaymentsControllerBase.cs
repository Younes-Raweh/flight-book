using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class BankPaymentsControllerBase : ControllerBase
{
    protected readonly IBankPaymentsService _service;

    public BankPaymentsControllerBase(IBankPaymentsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one BankPayment
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<BankPayment>> CreateBankPayment(BankPaymentCreateInput input)
    {
        var bankPayment = await _service.CreateBankPayment(input);

        return CreatedAtAction(nameof(BankPayment), new { id = bankPayment.Id }, bankPayment);
    }

    /// <summary>
    /// Delete one BankPayment
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteBankPayment(
        [FromRoute()] BankPaymentWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteBankPayment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many BankPayments
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<BankPayment>>> BankPayments(
        [FromQuery()] BankPaymentFindManyArgs filter
    )
    {
        return Ok(await _service.BankPayments(filter));
    }

    /// <summary>
    /// Meta data about BankPayment records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> BankPaymentsMeta(
        [FromQuery()] BankPaymentFindManyArgs filter
    )
    {
        return Ok(await _service.BankPaymentsMeta(filter));
    }

    /// <summary>
    /// Get one BankPayment
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<BankPayment>> BankPayment(
        [FromRoute()] BankPaymentWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.BankPayment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one BankPayment
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateBankPayment(
        [FromRoute()] BankPaymentWhereUniqueInput uniqueId,
        [FromQuery()] BankPaymentUpdateInput bankPaymentUpdateDto
    )
    {
        try
        {
            await _service.UpdateBankPayment(uniqueId, bankPaymentUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
