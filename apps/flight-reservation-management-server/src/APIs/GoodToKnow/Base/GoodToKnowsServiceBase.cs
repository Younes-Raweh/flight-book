using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class GoodToKnowsServiceBase : IGoodToKnowsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public GoodToKnowsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one GoodToKnow
    /// </summary>
    public async Task<GoodToKnow> CreateGoodToKnow(GoodToKnowCreateInput createDto)
    {
        var goodToKnow = new GoodToKnowDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            goodToKnow.Id = createDto.Id;
        }

        _context.GoodToKnows.Add(goodToKnow);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<GoodToKnowDbModel>(goodToKnow.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one GoodToKnow
    /// </summary>
    public async Task DeleteGoodToKnow(GoodToKnowWhereUniqueInput uniqueId)
    {
        var goodToKnow = await _context.GoodToKnows.FindAsync(uniqueId.Id);
        if (goodToKnow == null)
        {
            throw new NotFoundException();
        }

        _context.GoodToKnows.Remove(goodToKnow);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many GoodToKnows
    /// </summary>
    public async Task<List<GoodToKnow>> GoodToKnows(GoodToKnowFindManyArgs findManyArgs)
    {
        var goodToKnows = await _context
            .GoodToKnows.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return goodToKnows.ConvertAll(goodToKnow => goodToKnow.ToDto());
    }

    /// <summary>
    /// Meta data about GoodToKnow records
    /// </summary>
    public async Task<MetadataDto> GoodToKnowsMeta(GoodToKnowFindManyArgs findManyArgs)
    {
        var count = await _context.GoodToKnows.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one GoodToKnow
    /// </summary>
    public async Task<GoodToKnow> GoodToKnow(GoodToKnowWhereUniqueInput uniqueId)
    {
        var goodToKnows = await this.GoodToKnows(
            new GoodToKnowFindManyArgs { Where = new GoodToKnowWhereInput { Id = uniqueId.Id } }
        );
        var goodToKnow = goodToKnows.FirstOrDefault();
        if (goodToKnow == null)
        {
            throw new NotFoundException();
        }

        return goodToKnow;
    }

    /// <summary>
    /// Update one GoodToKnow
    /// </summary>
    public async Task UpdateGoodToKnow(
        GoodToKnowWhereUniqueInput uniqueId,
        GoodToKnowUpdateInput updateDto
    )
    {
        var goodToKnow = updateDto.ToModel(uniqueId);

        _context.Entry(goodToKnow).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.GoodToKnows.Any(e => e.Id == goodToKnow.Id))
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
