using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IReservationsService
{
    /// <summary>
    /// Create one Reservation
    /// </summary>
    public Task<Reservation> CreateReservation(ReservationCreateInput reservation);

    /// <summary>
    /// Delete one Reservation
    /// </summary>
    public Task DeleteReservation(ReservationWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Reservations
    /// </summary>
    public Task<List<Reservation>> Reservations(ReservationFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Reservation records
    /// </summary>
    public Task<MetadataDto> ReservationsMeta(ReservationFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Reservation
    /// </summary>
    public Task<Reservation> Reservation(ReservationWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Reservation
    /// </summary>
    public Task UpdateReservation(
        ReservationWhereUniqueInput uniqueId,
        ReservationUpdateInput updateDto
    );
}
