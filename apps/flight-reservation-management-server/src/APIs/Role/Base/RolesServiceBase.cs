using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class RolesServiceBase : IRolesService
{
    protected readonly FlightReservationManagementDbContext _context;

    public RolesServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Role
    /// </summary>
    public async Task<Role> CreateRole(RoleCreateInput createDto)
    {
        var role = new RoleDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            DisplayName = createDto.DisplayName,
            Name = createDto.Name,
            PermissionId = createDto.PermissionId,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            role.Id = createDto.Id;
        }
        if (createDto.Role != null)
        {
            role.Role = await _context
                .Roles.Where(role => createDto.Role.Id == role.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Roles != null)
        {
            role.Roles = await _context
                .Roles.Where(role => createDto.Roles.Select(t => t.Id).Contains(role.Id))
                .ToListAsync();
        }

        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<RoleDbModel>(role.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Role
    /// </summary>
    public async Task DeleteRole(RoleWhereUniqueInput uniqueId)
    {
        var role = await _context.Roles.FindAsync(uniqueId.Id);
        if (role == null)
        {
            throw new NotFoundException();
        }

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Roles
    /// </summary>
    public async Task<List<Role>> Roles(RoleFindManyArgs findManyArgs)
    {
        var roles = await _context
            .Roles.Include(x => x.Role)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return roles.ConvertAll(role => role.ToDto());
    }

    /// <summary>
    /// Meta data about Role records
    /// </summary>
    public async Task<MetadataDto> RolesMeta(RoleFindManyArgs findManyArgs)
    {
        var count = await _context.Roles.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Role
    /// </summary>
    public async Task<Role> Role(RoleWhereUniqueInput uniqueId)
    {
        var roles = await this.Roles(
            new RoleFindManyArgs { Where = new RoleWhereInput { Id = uniqueId.Id } }
        );
        var role = roles.FirstOrDefault();
        if (role == null)
        {
            throw new NotFoundException();
        }

        return role;
    }

    /// <summary>
    /// Update one Role
    /// </summary>
    public async Task UpdateRole(RoleWhereUniqueInput uniqueId, RoleUpdateInput updateDto)
    {
        var role = updateDto.ToModel(uniqueId);

        if (updateDto.Role != null)
        {
            role.Role = await _context
                .Roles.Where(role => updateDto.Role == role.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Roles != null)
        {
            role.Roles = await _context
                .Roles.Where(role => updateDto.Roles.Select(t => t).Contains(role.Id))
                .ToListAsync();
        }

        _context.Entry(role).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Roles.Any(e => e.Id == role.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a role_ record for Role
    /// </summary>
    public async Task<Role> GetRole(RoleWhereUniqueInput uniqueId)
    {
        var role = await _context
            .Roles.Where(role => role.Id == uniqueId.Id)
            .Include(role => role.Role)
            .FirstOrDefaultAsync();
        if (role == null)
        {
            throw new NotFoundException();
        }
        return role.Role.ToDto();
    }

    /// <summary>
    /// Connect multiple Roles records to Role
    /// </summary>
    public async Task ConnectRoles(
        RoleWhereUniqueInput uniqueId,
        RoleWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Roles.Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Roles.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Roles);

        foreach (var child in childrenToConnect)
        {
            parent.Roles.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Roles records from Role
    /// </summary>
    public async Task DisconnectRoles(
        RoleWhereUniqueInput uniqueId,
        RoleWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Roles.Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Roles.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Roles?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Roles records for Role
    /// </summary>
    public async Task<List<Role>> FindRoles(
        RoleWhereUniqueInput uniqueId,
        RoleFindManyArgs roleFindManyArgs
    )
    {
        var roles = await _context
            .Roles.Where(m => m.RoleId == uniqueId.Id)
            .ApplyWhere(roleFindManyArgs.Where)
            .ApplySkip(roleFindManyArgs.Skip)
            .ApplyTake(roleFindManyArgs.Take)
            .ApplyOrderBy(roleFindManyArgs.SortBy)
            .ToListAsync();

        return roles.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Roles records for Role
    /// </summary>
    public async Task UpdateRoles(RoleWhereUniqueInput uniqueId, RoleWhereUniqueInput[] childrenIds)
    {
        var role = await _context
            .Roles.Include(t => t.Roles)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (role == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Roles.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        role.Roles = children;
        await _context.SaveChangesAsync();
    }
}
