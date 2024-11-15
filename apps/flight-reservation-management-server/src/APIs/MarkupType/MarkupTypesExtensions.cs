using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class MarkupTypesExtensions
{
    public static MarkupType ToDto(this MarkupTypeDbModel model)
    {
        return new MarkupType
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static MarkupTypeDbModel ToModel(
        this MarkupTypeUpdateInput updateDto,
        MarkupTypeWhereUniqueInput uniqueId
    )
    {
        var markupType = new MarkupTypeDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            markupType.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            markupType.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return markupType;
    }
}
