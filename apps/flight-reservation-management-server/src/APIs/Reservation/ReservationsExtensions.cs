using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class ReservationsExtensions
{
    public static Reservation ToDto(this ReservationDbModel model)
    {
        return new Reservation
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ReservationDbModel ToModel(
        this ReservationUpdateInput updateDto,
        ReservationWhereUniqueInput uniqueId
    )
    {
        var reservation = new ReservationDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            reservation.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            reservation.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return reservation;
    }
}
