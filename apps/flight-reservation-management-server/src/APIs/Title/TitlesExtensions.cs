using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class TitlesExtensions
{
    public static Title ToDto(this TitleDbModel model)
    {
        return new Title
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static TitleDbModel ToModel(
        this TitleUpdateInput updateDto,
        TitleWhereUniqueInput uniqueId
    )
    {
        var title = new TitleDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            title.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            title.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return title;
    }
}
