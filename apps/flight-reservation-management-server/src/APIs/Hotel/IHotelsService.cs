using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IHotelsService
{
    /// <summary>
    /// Create one Hotel
    /// </summary>
    public Task<Hotel> CreateHotel(HotelCreateInput hotel);

    /// <summary>
    /// Delete one Hotel
    /// </summary>
    public Task DeleteHotel(HotelWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Hotels
    /// </summary>
    public Task<List<Hotel>> Hotels(HotelFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Hotel records
    /// </summary>
    public Task<MetadataDto> HotelsMeta(HotelFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Hotel
    /// </summary>
    public Task<Hotel> Hotel(HotelWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Hotel
    /// </summary>
    public Task UpdateHotel(HotelWhereUniqueInput uniqueId, HotelUpdateInput updateDto);
}
