using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ReservationsControllerBase : ControllerBase
{
    protected readonly IReservationsService _service;

    public ReservationsControllerBase(IReservationsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Reservation
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Reservation>> CreateReservation(ReservationCreateInput input)
    {
        var reservation = await _service.CreateReservation(input);

        return CreatedAtAction(nameof(Reservation), new { id = reservation.Id }, reservation);
    }

    /// <summary>
    /// Delete one Reservation
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteReservation(
        [FromRoute()] ReservationWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteReservation(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Reservations
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Reservation>>> Reservations(
        [FromQuery()] ReservationFindManyArgs filter
    )
    {
        return Ok(await _service.Reservations(filter));
    }

    /// <summary>
    /// Meta data about Reservation records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ReservationsMeta(
        [FromQuery()] ReservationFindManyArgs filter
    )
    {
        return Ok(await _service.ReservationsMeta(filter));
    }

    /// <summary>
    /// Get one Reservation
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Reservation>> Reservation(
        [FromRoute()] ReservationWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Reservation(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Reservation
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateReservation(
        [FromRoute()] ReservationWhereUniqueInput uniqueId,
        [FromQuery()] ReservationUpdateInput reservationUpdateDto
    )
    {
        try
        {
            await _service.UpdateReservation(uniqueId, reservationUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
