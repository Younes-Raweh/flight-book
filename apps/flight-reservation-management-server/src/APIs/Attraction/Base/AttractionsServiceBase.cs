using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class AttractionsServiceBase : IAttractionsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public AttractionsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Attraction
    /// </summary>
    public async Task<Attraction> CreateAttraction(AttractionCreateInput createDto)
    {
        var attraction = new AttractionDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            attraction.Id = createDto.Id;
        }

        _context.Attractions.Add(attraction);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<AttractionDbModel>(attraction.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Attraction
    /// </summary>
    public async Task DeleteAttraction(AttractionWhereUniqueInput uniqueId)
    {
        var attraction = await _context.Attractions.FindAsync(uniqueId.Id);
        if (attraction == null)
        {
            throw new NotFoundException();
        }

        _context.Attractions.Remove(attraction);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Attractions
    /// </summary>
    public async Task<List<Attraction>> Attractions(AttractionFindManyArgs findManyArgs)
    {
        var attractions = await _context
            .Attractions.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return attractions.ConvertAll(attraction => attraction.ToDto());
    }

    /// <summary>
    /// Meta data about Attraction records
    /// </summary>
    public async Task<MetadataDto> AttractionsMeta(AttractionFindManyArgs findManyArgs)
    {
        var count = await _context.Attractions.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Attraction
    /// </summary>
    public async Task<Attraction> Attraction(AttractionWhereUniqueInput uniqueId)
    {
        var attractions = await this.Attractions(
            new AttractionFindManyArgs { Where = new AttractionWhereInput { Id = uniqueId.Id } }
        );
        var attraction = attractions.FirstOrDefault();
        if (attraction == null)
        {
            throw new NotFoundException();
        }

        return attraction;
    }

    /// <summary>
    /// Update one Attraction
    /// </summary>
    public async Task UpdateAttraction(
        AttractionWhereUniqueInput uniqueId,
        AttractionUpdateInput updateDto
    )
    {
        var attraction = updateDto.ToModel(uniqueId);

        _context.Entry(attraction).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Attractions.Any(e => e.Id == attraction.Id))
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
