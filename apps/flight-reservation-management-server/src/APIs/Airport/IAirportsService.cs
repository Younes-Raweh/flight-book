using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IAirportsService
{
    /// <summary>
    /// Create one Airport
    /// </summary>
    public Task<Airport> CreateAirport(AirportCreateInput airport);

    /// <summary>
    /// Delete one Airport
    /// </summary>
    public Task DeleteAirport(AirportWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Airports
    /// </summary>
    public Task<List<Airport>> Airports(AirportFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Airport records
    /// </summary>
    public Task<MetadataDto> AirportsMeta(AirportFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Airport
    /// </summary>
    public Task<Airport> Airport(AirportWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Airport
    /// </summary>
    public Task UpdateAirport(AirportWhereUniqueInput uniqueId, AirportUpdateInput updateDto);
}
