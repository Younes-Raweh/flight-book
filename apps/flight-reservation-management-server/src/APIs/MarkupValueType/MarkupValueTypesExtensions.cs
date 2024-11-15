using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class MarkupValueTypesExtensions
{
    public static MarkupValueType ToDto(this MarkupValueTypeDbModel model)
    {
        return new MarkupValueType
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static MarkupValueTypeDbModel ToModel(
        this MarkupValueTypeUpdateInput updateDto,
        MarkupValueTypeWhereUniqueInput uniqueId
    )
    {
        var markupValueType = new MarkupValueTypeDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            markupValueType.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            markupValueType.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return markupValueType;
    }
}
