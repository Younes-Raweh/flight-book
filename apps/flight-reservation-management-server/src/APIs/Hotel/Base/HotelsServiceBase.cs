using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class HotelsServiceBase : IHotelsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public HotelsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Hotel
    /// </summary>
    public async Task<Hotel> CreateHotel(HotelCreateInput createDto)
    {
        var hotel = new HotelDbModel
        {
            Code = createDto.Code,
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            hotel.Id = createDto.Id;
        }

        _context.Hotels.Add(hotel);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<HotelDbModel>(hotel.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Hotel
    /// </summary>
    public async Task DeleteHotel(HotelWhereUniqueInput uniqueId)
    {
        var hotel = await _context.Hotels.FindAsync(uniqueId.Id);
        if (hotel == null)
        {
            throw new NotFoundException();
        }

        _context.Hotels.Remove(hotel);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Hotels
    /// </summary>
    public async Task<List<Hotel>> Hotels(HotelFindManyArgs findManyArgs)
    {
        var hotels = await _context
            .Hotels.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return hotels.ConvertAll(hotel => hotel.ToDto());
    }

    /// <summary>
    /// Meta data about Hotel records
    /// </summary>
    public async Task<MetadataDto> HotelsMeta(HotelFindManyArgs findManyArgs)
    {
        var count = await _context.Hotels.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Hotel
    /// </summary>
    public async Task<Hotel> Hotel(HotelWhereUniqueInput uniqueId)
    {
        var hotels = await this.Hotels(
            new HotelFindManyArgs { Where = new HotelWhereInput { Id = uniqueId.Id } }
        );
        var hotel = hotels.FirstOrDefault();
        if (hotel == null)
        {
            throw new NotFoundException();
        }

        return hotel;
    }

    /// <summary>
    /// Update one Hotel
    /// </summary>
    public async Task UpdateHotel(HotelWhereUniqueInput uniqueId, HotelUpdateInput updateDto)
    {
        var hotel = updateDto.ToModel(uniqueId);

        _context.Entry(hotel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Hotels.Any(e => e.Id == hotel.Id))
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
