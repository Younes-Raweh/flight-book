using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class HotelDealsServiceBase : IHotelDealsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public HotelDealsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one HotelDeal
    /// </summary>
    public async Task<HotelDeal> CreateHotelDeal(HotelDealCreateInput createDto)
    {
        var hotelDeal = new HotelDealDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            hotelDeal.Id = createDto.Id;
        }

        _context.HotelDeals.Add(hotelDeal);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<HotelDealDbModel>(hotelDeal.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one HotelDeal
    /// </summary>
    public async Task DeleteHotelDeal(HotelDealWhereUniqueInput uniqueId)
    {
        var hotelDeal = await _context.HotelDeals.FindAsync(uniqueId.Id);
        if (hotelDeal == null)
        {
            throw new NotFoundException();
        }

        _context.HotelDeals.Remove(hotelDeal);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many HotelDeals
    /// </summary>
    public async Task<List<HotelDeal>> HotelDeals(HotelDealFindManyArgs findManyArgs)
    {
        var hotelDeals = await _context
            .HotelDeals.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return hotelDeals.ConvertAll(hotelDeal => hotelDeal.ToDto());
    }

    /// <summary>
    /// Meta data about HotelDeal records
    /// </summary>
    public async Task<MetadataDto> HotelDealsMeta(HotelDealFindManyArgs findManyArgs)
    {
        var count = await _context.HotelDeals.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one HotelDeal
    /// </summary>
    public async Task<HotelDeal> HotelDeal(HotelDealWhereUniqueInput uniqueId)
    {
        var hotelDeals = await this.HotelDeals(
            new HotelDealFindManyArgs { Where = new HotelDealWhereInput { Id = uniqueId.Id } }
        );
        var hotelDeal = hotelDeals.FirstOrDefault();
        if (hotelDeal == null)
        {
            throw new NotFoundException();
        }

        return hotelDeal;
    }

    /// <summary>
    /// Update one HotelDeal
    /// </summary>
    public async Task UpdateHotelDeal(
        HotelDealWhereUniqueInput uniqueId,
        HotelDealUpdateInput updateDto
    )
    {
        var hotelDeal = updateDto.ToModel(uniqueId);

        _context.Entry(hotelDeal).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.HotelDeals.Any(e => e.Id == hotelDeal.Id))
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
