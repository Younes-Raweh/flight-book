using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class TourOperatorsExtensions
{
    public static TourOperator ToDto(this TourOperatorDbModel model)
    {
        return new TourOperator
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static TourOperatorDbModel ToModel(
        this TourOperatorUpdateInput updateDto,
        TourOperatorWhereUniqueInput uniqueId
    )
    {
        var tourOperator = new TourOperatorDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            tourOperator.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            tourOperator.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return tourOperator;
    }
}
