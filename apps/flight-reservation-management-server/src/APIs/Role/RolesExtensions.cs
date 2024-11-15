using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class RolesExtensions
{
    public static Role ToDto(this RoleDbModel model)
    {
        return new Role
        {
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            DisplayName = model.DisplayName,
            Id = model.Id,
            Name = model.Name,
            PermissionId = model.PermissionId,
            Role = model.RoleId,
            Roles = model.Roles?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static RoleDbModel ToModel(this RoleUpdateInput updateDto, RoleWhereUniqueInput uniqueId)
    {
        var role = new RoleDbModel
        {
            Id = uniqueId.Id,
            Description = updateDto.Description,
            DisplayName = updateDto.DisplayName,
            Name = updateDto.Name,
            PermissionId = updateDto.PermissionId
        };

        if (updateDto.CreatedAt != null)
        {
            role.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Role != null)
        {
            role.RoleId = updateDto.Role;
        }
        if (updateDto.UpdatedAt != null)
        {
            role.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return role;
    }
}
