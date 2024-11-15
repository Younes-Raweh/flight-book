using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class RoleUsersExtensions
{
    public static RoleUser ToDto(this RoleUserDbModel model)
    {
        return new RoleUser
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static RoleUserDbModel ToModel(
        this RoleUserUpdateInput updateDto,
        RoleUserWhereUniqueInput uniqueId
    )
    {
        var roleUser = new RoleUserDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            roleUser.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            roleUser.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return roleUser;
    }
}
