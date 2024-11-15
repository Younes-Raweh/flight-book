using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class PassengersExtensions
{
    public static Passenger ToDto(this PassengerDbModel model)
    {
        return new Passenger
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PassengerDbModel ToModel(
        this PassengerUpdateInput updateDto,
        PassengerWhereUniqueInput uniqueId
    )
    {
        var passenger = new PassengerDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            passenger.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            passenger.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return passenger;
    }
}
