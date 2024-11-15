using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class GoodToKnowsExtensions
{
    public static GoodToKnow ToDto(this GoodToKnowDbModel model)
    {
        return new GoodToKnow
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static GoodToKnowDbModel ToModel(
        this GoodToKnowUpdateInput updateDto,
        GoodToKnowWhereUniqueInput uniqueId
    )
    {
        var goodToKnow = new GoodToKnowDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            goodToKnow.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            goodToKnow.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return goodToKnow;
    }
}
