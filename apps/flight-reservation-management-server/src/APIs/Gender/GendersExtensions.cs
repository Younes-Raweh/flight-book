using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class GendersExtensions
{
    public static Gender ToDto(this GenderDbModel model)
    {
        return new Gender
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static GenderDbModel ToModel(
        this GenderUpdateInput updateDto,
        GenderWhereUniqueInput uniqueId
    )
    {
        var gender = new GenderDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            gender.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            gender.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return gender;
    }
}
