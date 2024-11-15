using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class FlightDealsServiceBase : IFlightDealsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public FlightDealsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one FlightDeal
    /// </summary>
    public async Task<FlightDeal> CreateFlightDeal(FlightDealCreateInput createDto)
    {
        var flightDeal = new FlightDealDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            flightDeal.Id = createDto.Id;
        }

        _context.FlightDeals.Add(flightDeal);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<FlightDealDbModel>(flightDeal.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one FlightDeal
    /// </summary>
    public async Task DeleteFlightDeal(FlightDealWhereUniqueInput uniqueId)
    {
        var flightDeal = await _context.FlightDeals.FindAsync(uniqueId.Id);
        if (flightDeal == null)
        {
            throw new NotFoundException();
        }

        _context.FlightDeals.Remove(flightDeal);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many FlightDeals
    /// </summary>
    public async Task<List<FlightDeal>> FlightDeals(FlightDealFindManyArgs findManyArgs)
    {
        var flightDeals = await _context
            .FlightDeals.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return flightDeals.ConvertAll(flightDeal => flightDeal.ToDto());
    }

    /// <summary>
    /// Meta data about FlightDeal records
    /// </summary>
    public async Task<MetadataDto> FlightDealsMeta(FlightDealFindManyArgs findManyArgs)
    {
        var count = await _context.FlightDeals.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one FlightDeal
    /// </summary>
    public async Task<FlightDeal> FlightDeal(FlightDealWhereUniqueInput uniqueId)
    {
        var flightDeals = await this.FlightDeals(
            new FlightDealFindManyArgs { Where = new FlightDealWhereInput { Id = uniqueId.Id } }
        );
        var flightDeal = flightDeals.FirstOrDefault();
        if (flightDeal == null)
        {
            throw new NotFoundException();
        }

        return flightDeal;
    }

    /// <summary>
    /// Update one FlightDeal
    /// </summary>
    public async Task UpdateFlightDeal(
        FlightDealWhereUniqueInput uniqueId,
        FlightDealUpdateInput updateDto
    )
    {
        var flightDeal = updateDto.ToModel(uniqueId);

        _context.Entry(flightDeal).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.FlightDeals.Any(e => e.Id == flightDeal.Id))
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
