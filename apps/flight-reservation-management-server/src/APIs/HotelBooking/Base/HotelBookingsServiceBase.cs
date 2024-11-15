using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class HotelBookingsServiceBase : IHotelBookingsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public HotelBookingsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one HotelBooking
    /// </summary>
    public async Task<HotelBooking> CreateHotelBooking(HotelBookingCreateInput createDto)
    {
        var hotelBooking = new HotelBookingDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            hotelBooking.Id = createDto.Id;
        }

        _context.HotelBookings.Add(hotelBooking);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<HotelBookingDbModel>(hotelBooking.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one HotelBooking
    /// </summary>
    public async Task DeleteHotelBooking(HotelBookingWhereUniqueInput uniqueId)
    {
        var hotelBooking = await _context.HotelBookings.FindAsync(uniqueId.Id);
        if (hotelBooking == null)
        {
            throw new NotFoundException();
        }

        _context.HotelBookings.Remove(hotelBooking);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many HotelBookings
    /// </summary>
    public async Task<List<HotelBooking>> HotelBookings(HotelBookingFindManyArgs findManyArgs)
    {
        var hotelBookings = await _context
            .HotelBookings.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return hotelBookings.ConvertAll(hotelBooking => hotelBooking.ToDto());
    }

    /// <summary>
    /// Meta data about HotelBooking records
    /// </summary>
    public async Task<MetadataDto> HotelBookingsMeta(HotelBookingFindManyArgs findManyArgs)
    {
        var count = await _context.HotelBookings.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one HotelBooking
    /// </summary>
    public async Task<HotelBooking> HotelBooking(HotelBookingWhereUniqueInput uniqueId)
    {
        var hotelBookings = await this.HotelBookings(
            new HotelBookingFindManyArgs { Where = new HotelBookingWhereInput { Id = uniqueId.Id } }
        );
        var hotelBooking = hotelBookings.FirstOrDefault();
        if (hotelBooking == null)
        {
            throw new NotFoundException();
        }

        return hotelBooking;
    }

    /// <summary>
    /// Update one HotelBooking
    /// </summary>
    public async Task UpdateHotelBooking(
        HotelBookingWhereUniqueInput uniqueId,
        HotelBookingUpdateInput updateDto
    )
    {
        var hotelBooking = updateDto.ToModel(uniqueId);

        _context.Entry(hotelBooking).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.HotelBookings.Any(e => e.Id == hotelBooking.Id))
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
