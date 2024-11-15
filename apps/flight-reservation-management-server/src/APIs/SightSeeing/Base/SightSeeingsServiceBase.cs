using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class SightSeeingsServiceBase : ISightSeeingsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public SightSeeingsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one SightSeeing
    /// </summary>
    public async Task<SightSeeing> CreateSightSeeing(SightSeeingCreateInput createDto)
    {
        var sightSeeing = new SightSeeingDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            sightSeeing.Id = createDto.Id;
        }

        _context.SightSeeings.Add(sightSeeing);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<SightSeeingDbModel>(sightSeeing.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one SightSeeing
    /// </summary>
    public async Task DeleteSightSeeing(SightSeeingWhereUniqueInput uniqueId)
    {
        var sightSeeing = await _context.SightSeeings.FindAsync(uniqueId.Id);
        if (sightSeeing == null)
        {
            throw new NotFoundException();
        }

        _context.SightSeeings.Remove(sightSeeing);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many SightSeeings
    /// </summary>
    public async Task<List<SightSeeing>> SightSeeings(SightSeeingFindManyArgs findManyArgs)
    {
        var sightSeeings = await _context
            .SightSeeings.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return sightSeeings.ConvertAll(sightSeeing => sightSeeing.ToDto());
    }

    /// <summary>
    /// Meta data about SightSeeing records
    /// </summary>
    public async Task<MetadataDto> SightSeeingsMeta(SightSeeingFindManyArgs findManyArgs)
    {
        var count = await _context.SightSeeings.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one SightSeeing
    /// </summary>
    public async Task<SightSeeing> SightSeeing(SightSeeingWhereUniqueInput uniqueId)
    {
        var sightSeeings = await this.SightSeeings(
            new SightSeeingFindManyArgs { Where = new SightSeeingWhereInput { Id = uniqueId.Id } }
        );
        var sightSeeing = sightSeeings.FirstOrDefault();
        if (sightSeeing == null)
        {
            throw new NotFoundException();
        }

        return sightSeeing;
    }

    /// <summary>
    /// Update one SightSeeing
    /// </summary>
    public async Task UpdateSightSeeing(
        SightSeeingWhereUniqueInput uniqueId,
        SightSeeingUpdateInput updateDto
    )
    {
        var sightSeeing = updateDto.ToModel(uniqueId);

        _context.Entry(sightSeeing).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.SightSeeings.Any(e => e.Id == sightSeeing.Id))
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
