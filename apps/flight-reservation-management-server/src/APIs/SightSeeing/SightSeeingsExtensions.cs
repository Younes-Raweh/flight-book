using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class SightSeeingsExtensions
{
    public static SightSeeing ToDto(this SightSeeingDbModel model)
    {
        return new SightSeeing
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static SightSeeingDbModel ToModel(
        this SightSeeingUpdateInput updateDto,
        SightSeeingWhereUniqueInput uniqueId
    )
    {
        var sightSeeing = new SightSeeingDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            sightSeeing.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            sightSeeing.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return sightSeeing;
    }
}
