using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IFlightDealsService
{
    /// <summary>
    /// Create one FlightDeal
    /// </summary>
    public Task<FlightDeal> CreateFlightDeal(FlightDealCreateInput flightdeal);

    /// <summary>
    /// Delete one FlightDeal
    /// </summary>
    public Task DeleteFlightDeal(FlightDealWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many FlightDeals
    /// </summary>
    public Task<List<FlightDeal>> FlightDeals(FlightDealFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about FlightDeal records
    /// </summary>
    public Task<MetadataDto> FlightDealsMeta(FlightDealFindManyArgs findManyArgs);

    /// <summary>
    /// Get one FlightDeal
    /// </summary>
    public Task<FlightDeal> FlightDeal(FlightDealWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one FlightDeal
    /// </summary>
    public Task UpdateFlightDeal(
        FlightDealWhereUniqueInput uniqueId,
        FlightDealUpdateInput updateDto
    );
}
