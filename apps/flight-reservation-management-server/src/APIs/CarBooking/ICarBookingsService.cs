using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface ICarBookingsService
{
    /// <summary>
    /// Create one CarBooking
    /// </summary>
    public Task<CarBooking> CreateCarBooking(CarBookingCreateInput carbooking);

    /// <summary>
    /// Delete one CarBooking
    /// </summary>
    public Task DeleteCarBooking(CarBookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many CarBookings
    /// </summary>
    public Task<List<CarBooking>> CarBookings(CarBookingFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about CarBooking records
    /// </summary>
    public Task<MetadataDto> CarBookingsMeta(CarBookingFindManyArgs findManyArgs);

    /// <summary>
    /// Get one CarBooking
    /// </summary>
    public Task<CarBooking> CarBooking(CarBookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one CarBooking
    /// </summary>
    public Task UpdateCarBooking(
        CarBookingWhereUniqueInput uniqueId,
        CarBookingUpdateInput updateDto
    );
}
