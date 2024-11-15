using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class MarkupsExtensions
{
    public static Markup ToDto(this MarkupDbModel model)
    {
        return new Markup
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static MarkupDbModel ToModel(
        this MarkupUpdateInput updateDto,
        MarkupWhereUniqueInput uniqueId
    )
    {
        var markup = new MarkupDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            markup.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            markup.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return markup;
    }
}
