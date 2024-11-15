using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class ProfilesServiceBase : IProfilesService
{
    protected readonly FlightReservationManagementDbContext _context;

    public ProfilesServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Profile
    /// </summary>
    public async Task<Profile> CreateProfile(ProfileCreateInput createDto)
    {
        var profile = new ProfileDbModel
        {
            Address = createDto.Address,
            CreatedAt = createDto.CreatedAt,
            FirstName = createDto.FirstName,
            GenderId = createDto.GenderId,
            OtherName = createDto.OtherName,
            PhoneNumber = createDto.PhoneNumber,
            Photo = createDto.Photo,
            SurName = createDto.SurName,
            TitleId = createDto.TitleId,
            UpdatedAt = createDto.UpdatedAt,
            UserId = createDto.UserId
        };

        if (createDto.Id != null)
        {
            profile.Id = createDto.Id;
        }

        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ProfileDbModel>(profile.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Profile
    /// </summary>
    public async Task DeleteProfile(ProfileWhereUniqueInput uniqueId)
    {
        var profile = await _context.Profiles.FindAsync(uniqueId.Id);
        if (profile == null)
        {
            throw new NotFoundException();
        }

        _context.Profiles.Remove(profile);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Profiles
    /// </summary>
    public async Task<List<Profile>> Profiles(ProfileFindManyArgs findManyArgs)
    {
        var profiles = await _context
            .Profiles.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return profiles.ConvertAll(profile => profile.ToDto());
    }

    /// <summary>
    /// Meta data about Profile records
    /// </summary>
    public async Task<MetadataDto> ProfilesMeta(ProfileFindManyArgs findManyArgs)
    {
        var count = await _context.Profiles.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Profile
    /// </summary>
    public async Task<Profile> Profile(ProfileWhereUniqueInput uniqueId)
    {
        var profiles = await this.Profiles(
            new ProfileFindManyArgs { Where = new ProfileWhereInput { Id = uniqueId.Id } }
        );
        var profile = profiles.FirstOrDefault();
        if (profile == null)
        {
            throw new NotFoundException();
        }

        return profile;
    }

    /// <summary>
    /// Update one Profile
    /// </summary>
    public async Task UpdateProfile(ProfileWhereUniqueInput uniqueId, ProfileUpdateInput updateDto)
    {
        var profile = updateDto.ToModel(uniqueId);

        _context.Entry(profile).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Profiles.Any(e => e.Id == profile.Id))
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
