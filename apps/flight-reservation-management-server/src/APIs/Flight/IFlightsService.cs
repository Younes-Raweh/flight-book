using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IFlightsService
{
    /// <summary>
    /// Create one Flight
    /// </summary>
    public Task<Flight> CreateFlight(FlightCreateInput flight);

    /// <summary>
    /// Delete one Flight
    /// </summary>
    public Task DeleteFlight(FlightWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Flights
    /// </summary>
    public Task<List<Flight>> Flights(FlightFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Flight records
    /// </summary>
    public Task<MetadataDto> FlightsMeta(FlightFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Flight
    /// </summary>
    public Task<Flight> Flight(FlightWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Flight
    /// </summary>
    public Task UpdateFlight(FlightWhereUniqueInput uniqueId, FlightUpdateInput updateDto);
}
