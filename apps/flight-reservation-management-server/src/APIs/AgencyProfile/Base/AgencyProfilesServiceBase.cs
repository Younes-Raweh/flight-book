using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class AgencyProfilesServiceBase : IAgencyProfilesService
{
    protected readonly FlightReservationManagementDbContext _context;

    public AgencyProfilesServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one AgencyProfile
    /// </summary>
    public async Task<AgencyProfile> CreateAgencyProfile(AgencyProfileCreateInput createDto)
    {
        var agencyProfile = new AgencyProfileDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            agencyProfile.Id = createDto.Id;
        }

        _context.AgencyProfiles.Add(agencyProfile);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<AgencyProfileDbModel>(agencyProfile.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one AgencyProfile
    /// </summary>
    public async Task DeleteAgencyProfile(AgencyProfileWhereUniqueInput uniqueId)
    {
        var agencyProfile = await _context.AgencyProfiles.FindAsync(uniqueId.Id);
        if (agencyProfile == null)
        {
            throw new NotFoundException();
        }

        _context.AgencyProfiles.Remove(agencyProfile);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many AgencyProfiles
    /// </summary>
    public async Task<List<AgencyProfile>> AgencyProfiles(AgencyProfileFindManyArgs findManyArgs)
    {
        var agencyProfiles = await _context
            .AgencyProfiles.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return agencyProfiles.ConvertAll(agencyProfile => agencyProfile.ToDto());
    }

    /// <summary>
    /// Meta data about AgencyProfile records
    /// </summary>
    public async Task<MetadataDto> AgencyProfilesMeta(AgencyProfileFindManyArgs findManyArgs)
    {
        var count = await _context.AgencyProfiles.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one AgencyProfile
    /// </summary>
    public async Task<AgencyProfile> AgencyProfile(AgencyProfileWhereUniqueInput uniqueId)
    {
        var agencyProfiles = await this.AgencyProfiles(
            new AgencyProfileFindManyArgs
            {
                Where = new AgencyProfileWhereInput { Id = uniqueId.Id }
            }
        );
        var agencyProfile = agencyProfiles.FirstOrDefault();
        if (agencyProfile == null)
        {
            throw new NotFoundException();
        }

        return agencyProfile;
    }

    /// <summary>
    /// Update one AgencyProfile
    /// </summary>
    public async Task UpdateAgencyProfile(
        AgencyProfileWhereUniqueInput uniqueId,
        AgencyProfileUpdateInput updateDto
    )
    {
        var agencyProfile = updateDto.ToModel(uniqueId);

        _context.Entry(agencyProfile).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.AgencyProfiles.Any(e => e.Id == agencyProfile.Id))
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
