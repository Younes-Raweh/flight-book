using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class HotelBookingsExtensions
{
    public static HotelBooking ToDto(this HotelBookingDbModel model)
    {
        return new HotelBooking
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static HotelBookingDbModel ToModel(
        this HotelBookingUpdateInput updateDto,
        HotelBookingWhereUniqueInput uniqueId
    )
    {
        var hotelBooking = new HotelBookingDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            hotelBooking.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            hotelBooking.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return hotelBooking;
    }
}
