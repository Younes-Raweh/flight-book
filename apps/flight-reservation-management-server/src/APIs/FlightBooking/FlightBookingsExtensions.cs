using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class FlightBookingsExtensions
{
    public static FlightBooking ToDto(this FlightBookingDbModel model)
    {
        return new FlightBooking
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static FlightBookingDbModel ToModel(
        this FlightBookingUpdateInput updateDto,
        FlightBookingWhereUniqueInput uniqueId
    )
    {
        var flightBooking = new FlightBookingDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            flightBooking.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            flightBooking.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return flightBooking;
    }
}
