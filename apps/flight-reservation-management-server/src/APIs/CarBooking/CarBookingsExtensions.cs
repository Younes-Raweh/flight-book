using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class CarBookingsExtensions
{
    public static CarBooking ToDto(this CarBookingDbModel model)
    {
        return new CarBooking
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static CarBookingDbModel ToModel(
        this CarBookingUpdateInput updateDto,
        CarBookingWhereUniqueInput uniqueId
    )
    {
        var carBooking = new CarBookingDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            carBooking.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            carBooking.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return carBooking;
    }
}
