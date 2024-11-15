using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class HotelDealsExtensions
{
    public static HotelDeal ToDto(this HotelDealDbModel model)
    {
        return new HotelDeal
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static HotelDealDbModel ToModel(
        this HotelDealUpdateInput updateDto,
        HotelDealWhereUniqueInput uniqueId
    )
    {
        var hotelDeal = new HotelDealDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            hotelDeal.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            hotelDeal.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return hotelDeal;
    }
}
