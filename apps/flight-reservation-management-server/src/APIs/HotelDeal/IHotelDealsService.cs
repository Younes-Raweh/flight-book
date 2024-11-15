using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IHotelDealsService
{
    /// <summary>
    /// Create one HotelDeal
    /// </summary>
    public Task<HotelDeal> CreateHotelDeal(HotelDealCreateInput hoteldeal);

    /// <summary>
    /// Delete one HotelDeal
    /// </summary>
    public Task DeleteHotelDeal(HotelDealWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many HotelDeals
    /// </summary>
    public Task<List<HotelDeal>> HotelDeals(HotelDealFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about HotelDeal records
    /// </summary>
    public Task<MetadataDto> HotelDealsMeta(HotelDealFindManyArgs findManyArgs);

    /// <summary>
    /// Get one HotelDeal
    /// </summary>
    public Task<HotelDeal> HotelDeal(HotelDealWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one HotelDeal
    /// </summary>
    public Task UpdateHotelDeal(HotelDealWhereUniqueInput uniqueId, HotelDealUpdateInput updateDto);
}
