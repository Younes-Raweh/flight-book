using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IRoleUsersService
{
    /// <summary>
    /// Create one RoleUser
    /// </summary>
    public Task<RoleUser> CreateRoleUser(RoleUserCreateInput roleuser);

    /// <summary>
    /// Delete one RoleUser
    /// </summary>
    public Task DeleteRoleUser(RoleUserWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many RoleUsers
    /// </summary>
    public Task<List<RoleUser>> RoleUsers(RoleUserFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about RoleUser records
    /// </summary>
    public Task<MetadataDto> RoleUsersMeta(RoleUserFindManyArgs findManyArgs);

    /// <summary>
    /// Get one RoleUser
    /// </summary>
    public Task<RoleUser> RoleUser(RoleUserWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one RoleUser
    /// </summary>
    public Task UpdateRoleUser(RoleUserWhereUniqueInput uniqueId, RoleUserUpdateInput updateDto);
}
