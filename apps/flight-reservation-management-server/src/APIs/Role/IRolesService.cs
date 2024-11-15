using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IRolesService
{
    /// <summary>
    /// Create one Role
    /// </summary>
    public Task<Role> CreateRole(RoleCreateInput role);

    /// <summary>
    /// Delete one Role
    /// </summary>
    public Task DeleteRole(RoleWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Roles
    /// </summary>
    public Task<List<Role>> Roles(RoleFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Role records
    /// </summary>
    public Task<MetadataDto> RolesMeta(RoleFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Role
    /// </summary>
    public Task<Role> Role(RoleWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Role
    /// </summary>
    public Task UpdateRole(RoleWhereUniqueInput uniqueId, RoleUpdateInput updateDto);

    /// <summary>
    /// Get a role_ record for Role
    /// </summary>
    public Task<Role> GetRole(RoleWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Roles records to Role
    /// </summary>
    public Task ConnectRoles(RoleWhereUniqueInput uniqueId, RoleWhereUniqueInput[] rolesId);

    /// <summary>
    /// Disconnect multiple Roles records from Role
    /// </summary>
    public Task DisconnectRoles(RoleWhereUniqueInput uniqueId, RoleWhereUniqueInput[] rolesId);

    /// <summary>
    /// Find multiple Roles records for Role
    /// </summary>
    public Task<List<Role>> FindRoles(
        RoleWhereUniqueInput uniqueId,
        RoleFindManyArgs RoleFindManyArgs
    );

    /// <summary>
    /// Update multiple Roles records for Role
    /// </summary>
    public Task UpdateRoles(RoleWhereUniqueInput uniqueId, RoleWhereUniqueInput[] rolesId);
}
