using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IAirlinesService
{
    /// <summary>
    /// Create one Airline
    /// </summary>
    public Task<Airline> CreateAirline(AirlineCreateInput airline);

    /// <summary>
    /// Delete one Airline
    /// </summary>
    public Task DeleteAirline(AirlineWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Airlines
    /// </summary>
    public Task<List<Airline>> Airlines(AirlineFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Airline records
    /// </summary>
    public Task<MetadataDto> AirlinesMeta(AirlineFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Airline
    /// </summary>
    public Task<Airline> Airline(AirlineWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Airline
    /// </summary>
    public Task UpdateAirline(AirlineWhereUniqueInput uniqueId, AirlineUpdateInput updateDto);
}
