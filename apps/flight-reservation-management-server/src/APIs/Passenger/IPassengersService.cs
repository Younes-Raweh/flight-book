using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IPassengersService
{
    /// <summary>
    /// Create one Passenger
    /// </summary>
    public Task<Passenger> CreatePassenger(PassengerCreateInput passenger);

    /// <summary>
    /// Delete one Passenger
    /// </summary>
    public Task DeletePassenger(PassengerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Passengers
    /// </summary>
    public Task<List<Passenger>> Passengers(PassengerFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Passenger records
    /// </summary>
    public Task<MetadataDto> PassengersMeta(PassengerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Passenger
    /// </summary>
    public Task<Passenger> Passenger(PassengerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Passenger
    /// </summary>
    public Task UpdatePassenger(PassengerWhereUniqueInput uniqueId, PassengerUpdateInput updateDto);
}
