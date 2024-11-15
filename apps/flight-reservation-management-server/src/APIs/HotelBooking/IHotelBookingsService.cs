using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IHotelBookingsService
{
    /// <summary>
    /// Create one HotelBooking
    /// </summary>
    public Task<HotelBooking> CreateHotelBooking(HotelBookingCreateInput hotelbooking);

    /// <summary>
    /// Delete one HotelBooking
    /// </summary>
    public Task DeleteHotelBooking(HotelBookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many HotelBookings
    /// </summary>
    public Task<List<HotelBooking>> HotelBookings(HotelBookingFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about HotelBooking records
    /// </summary>
    public Task<MetadataDto> HotelBookingsMeta(HotelBookingFindManyArgs findManyArgs);

    /// <summary>
    /// Get one HotelBooking
    /// </summary>
    public Task<HotelBooking> HotelBooking(HotelBookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one HotelBooking
    /// </summary>
    public Task UpdateHotelBooking(
        HotelBookingWhereUniqueInput uniqueId,
        HotelBookingUpdateInput updateDto
    );
}
