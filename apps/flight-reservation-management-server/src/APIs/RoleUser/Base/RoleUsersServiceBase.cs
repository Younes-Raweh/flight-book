using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class RoleUsersServiceBase : IRoleUsersService
{
    protected readonly FlightReservationManagementDbContext _context;

    public RoleUsersServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one RoleUser
    /// </summary>
    public async Task<RoleUser> CreateRoleUser(RoleUserCreateInput createDto)
    {
        var roleUser = new RoleUserDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            roleUser.Id = createDto.Id;
        }

        _context.RoleUsers.Add(roleUser);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<RoleUserDbModel>(roleUser.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one RoleUser
    /// </summary>
    public async Task DeleteRoleUser(RoleUserWhereUniqueInput uniqueId)
    {
        var roleUser = await _context.RoleUsers.FindAsync(uniqueId.Id);
        if (roleUser == null)
        {
            throw new NotFoundException();
        }

        _context.RoleUsers.Remove(roleUser);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many RoleUsers
    /// </summary>
    public async Task<List<RoleUser>> RoleUsers(RoleUserFindManyArgs findManyArgs)
    {
        var roleUsers = await _context
            .RoleUsers.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return roleUsers.ConvertAll(roleUser => roleUser.ToDto());
    }

    /// <summary>
    /// Meta data about RoleUser records
    /// </summary>
    public async Task<MetadataDto> RoleUsersMeta(RoleUserFindManyArgs findManyArgs)
    {
        var count = await _context.RoleUsers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one RoleUser
    /// </summary>
    public async Task<RoleUser> RoleUser(RoleUserWhereUniqueInput uniqueId)
    {
        var roleUsers = await this.RoleUsers(
            new RoleUserFindManyArgs { Where = new RoleUserWhereInput { Id = uniqueId.Id } }
        );
        var roleUser = roleUsers.FirstOrDefault();
        if (roleUser == null)
        {
            throw new NotFoundException();
        }

        return roleUser;
    }

    /// <summary>
    /// Update one RoleUser
    /// </summary>
    public async Task UpdateRoleUser(
        RoleUserWhereUniqueInput uniqueId,
        RoleUserUpdateInput updateDto
    )
    {
        var roleUser = updateDto.ToModel(uniqueId);

        _context.Entry(roleUser).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.RoleUsers.Any(e => e.Id == roleUser.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
