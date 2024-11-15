using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class PayLatersExtensions
{
    public static PayLater ToDto(this PayLaterDbModel model)
    {
        return new PayLater
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PayLaterDbModel ToModel(
        this PayLaterUpdateInput updateDto,
        PayLaterWhereUniqueInput uniqueId
    )
    {
        var payLater = new PayLaterDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            payLater.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            payLater.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return payLater;
    }
}
