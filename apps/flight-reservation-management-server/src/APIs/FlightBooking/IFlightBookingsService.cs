using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IFlightBookingsService
{
    /// <summary>
    /// Create one FlightBooking
    /// </summary>
    public Task<FlightBooking> CreateFlightBooking(FlightBookingCreateInput flightbooking);

    /// <summary>
    /// Delete one FlightBooking
    /// </summary>
    public Task DeleteFlightBooking(FlightBookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many FlightBookings
    /// </summary>
    public Task<List<FlightBooking>> FlightBookings(FlightBookingFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about FlightBooking records
    /// </summary>
    public Task<MetadataDto> FlightBookingsMeta(FlightBookingFindManyArgs findManyArgs);

    /// <summary>
    /// Get one FlightBooking
    /// </summary>
    public Task<FlightBooking> FlightBooking(FlightBookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one FlightBooking
    /// </summary>
    public Task UpdateFlightBooking(
        FlightBookingWhereUniqueInput uniqueId,
        FlightBookingUpdateInput updateDto
    );
}
