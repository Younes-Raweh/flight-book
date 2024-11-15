using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class AttractionsExtensions
{
    public static Attraction ToDto(this AttractionDbModel model)
    {
        return new Attraction
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static AttractionDbModel ToModel(
        this AttractionUpdateInput updateDto,
        AttractionWhereUniqueInput uniqueId
    )
    {
        var attraction = new AttractionDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            attraction.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            attraction.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return attraction;
    }
}
