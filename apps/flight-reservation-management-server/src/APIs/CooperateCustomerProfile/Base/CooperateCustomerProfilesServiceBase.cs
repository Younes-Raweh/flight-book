using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class CooperateCustomerProfilesServiceBase : ICooperateCustomerProfilesService
{
    protected readonly FlightReservationManagementDbContext _context;

    public CooperateCustomerProfilesServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one CooperateCustomerProfile
    /// </summary>
    public async Task<CooperateCustomerProfile> CreateCooperateCustomerProfile(
        CooperateCustomerProfileCreateInput createDto
    )
    {
        var cooperateCustomerProfile = new CooperateCustomerProfileDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            cooperateCustomerProfile.Id = createDto.Id;
        }

        _context.CooperateCustomerProfiles.Add(cooperateCustomerProfile);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CooperateCustomerProfileDbModel>(
            cooperateCustomerProfile.Id
        );

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one CooperateCustomerProfile
    /// </summary>
    public async Task DeleteCooperateCustomerProfile(
        CooperateCustomerProfileWhereUniqueInput uniqueId
    )
    {
        var cooperateCustomerProfile = await _context.CooperateCustomerProfiles.FindAsync(
            uniqueId.Id
        );
        if (cooperateCustomerProfile == null)
        {
            throw new NotFoundException();
        }

        _context.CooperateCustomerProfiles.Remove(cooperateCustomerProfile);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many CooperateCustomerProfiles
    /// </summary>
    public async Task<List<CooperateCustomerProfile>> CooperateCustomerProfiles(
        CooperateCustomerProfileFindManyArgs findManyArgs
    )
    {
        var cooperateCustomerProfiles = await _context
            .CooperateCustomerProfiles.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return cooperateCustomerProfiles.ConvertAll(cooperateCustomerProfile =>
            cooperateCustomerProfile.ToDto()
        );
    }

    /// <summary>
    /// Meta data about CooperateCustomerProfile records
    /// </summary>
    public async Task<MetadataDto> CooperateCustomerProfilesMeta(
        CooperateCustomerProfileFindManyArgs findManyArgs
    )
    {
        var count = await _context
            .CooperateCustomerProfiles.ApplyWhere(findManyArgs.Where)
            .CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one CooperateCustomerProfile
    /// </summary>
    public async Task<CooperateCustomerProfile> CooperateCustomerProfile(
        CooperateCustomerProfileWhereUniqueInput uniqueId
    )
    {
        var cooperateCustomerProfiles = await this.CooperateCustomerProfiles(
            new CooperateCustomerProfileFindManyArgs
            {
                Where = new CooperateCustomerProfileWhereInput { Id = uniqueId.Id }
            }
        );
        var cooperateCustomerProfile = cooperateCustomerProfiles.FirstOrDefault();
        if (cooperateCustomerProfile == null)
        {
            throw new NotFoundException();
        }

        return cooperateCustomerProfile;
    }

    /// <summary>
    /// Update one CooperateCustomerProfile
    /// </summary>
    public async Task UpdateCooperateCustomerProfile(
        CooperateCustomerProfileWhereUniqueInput uniqueId,
        CooperateCustomerProfileUpdateInput updateDto
    )
    {
        var cooperateCustomerProfile = updateDto.ToModel(uniqueId);

        _context.Entry(cooperateCustomerProfile).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.CooperateCustomerProfiles.Any(e => e.Id == cooperateCustomerProfile.Id))
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
