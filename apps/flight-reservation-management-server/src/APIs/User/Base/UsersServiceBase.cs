using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class UsersServiceBase : IUsersService
{
    protected readonly FlightReservationManagementDbContext _context;

    public UsersServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one User
    /// </summary>
    public async Task<User> CreateUser(UserCreateInput createDto)
    {
        var user = new UserDbModel
        {
            ApiToken = createDto.ApiToken,
            CreatedAt = createDto.CreatedAt,
            DeleteStatus = createDto.DeleteStatus,
            Email = createDto.Email,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Password = createDto.Password,
            ProfileCompleteStatus = createDto.ProfileCompleteStatus,
            Roles = createDto.Roles,
            UpdatedAt = createDto.UpdatedAt,
            Username = createDto.Username
        };

        if (createDto.Id != null)
        {
            user.Id = createDto.Id;
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<UserDbModel>(user.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one User
    /// </summary>
    public async Task DeleteUser(UserWhereUniqueInput uniqueId)
    {
        var user = await _context.Users.FindAsync(uniqueId.Id);
        if (user == null)
        {
            throw new NotFoundException();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Users
    /// </summary>
    public async Task<List<User>> Users(UserFindManyArgs findManyArgs)
    {
        var users = await _context
            .Users.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return users.ConvertAll(user => user.ToDto());
    }

    /// <summary>
    /// Meta data about User records
    /// </summary>
    public async Task<MetadataDto> UsersMeta(UserFindManyArgs findManyArgs)
    {
        var count = await _context.Users.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one User
    /// </summary>
    public async Task<User> User(UserWhereUniqueInput uniqueId)
    {
        var users = await this.Users(
            new UserFindManyArgs { Where = new UserWhereInput { Id = uniqueId.Id } }
        );
        var user = users.FirstOrDefault();
        if (user == null)
        {
            throw new NotFoundException();
        }

        return user;
    }

    /// <summary>
    /// Update one User
    /// </summary>
    public async Task UpdateUser(UserWhereUniqueInput uniqueId, UserUpdateInput updateDto)
    {
        var user = updateDto.ToModel(uniqueId);

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Users.Any(e => e.Id == user.Id))
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