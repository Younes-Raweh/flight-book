using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class FlightBookingsServiceBase : IFlightBookingsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public FlightBookingsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one FlightBooking
    /// </summary>
    public async Task<FlightBooking> CreateFlightBooking(FlightBookingCreateInput createDto)
    {
        var flightBooking = new FlightBookingDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            flightBooking.Id = createDto.Id;
        }

        _context.FlightBookings.Add(flightBooking);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<FlightBookingDbModel>(flightBooking.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one FlightBooking
    /// </summary>
    public async Task DeleteFlightBooking(FlightBookingWhereUniqueInput uniqueId)
    {
        var flightBooking = await _context.FlightBookings.FindAsync(uniqueId.Id);
        if (flightBooking == null)
        {
            throw new NotFoundException();
        }

        _context.FlightBookings.Remove(flightBooking);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many FlightBookings
    /// </summary>
    public async Task<List<FlightBooking>> FlightBookings(FlightBookingFindManyArgs findManyArgs)
    {
        var flightBookings = await _context
            .FlightBookings.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return flightBookings.ConvertAll(flightBooking => flightBooking.ToDto());
    }

    /// <summary>
    /// Meta data about FlightBooking records
    /// </summary>
    public async Task<MetadataDto> FlightBookingsMeta(FlightBookingFindManyArgs findManyArgs)
    {
        var count = await _context.FlightBookings.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one FlightBooking
    /// </summary>
    public async Task<FlightBooking> FlightBooking(FlightBookingWhereUniqueInput uniqueId)
    {
        var flightBookings = await this.FlightBookings(
            new FlightBookingFindManyArgs
            {
                Where = new FlightBookingWhereInput { Id = uniqueId.Id }
            }
        );
        var flightBooking = flightBookings.FirstOrDefault();
        if (flightBooking == null)
        {
            throw new NotFoundException();
        }

        return flightBooking;
    }

    /// <summary>
    /// Update one FlightBooking
    /// </summary>
    public async Task UpdateFlightBooking(
        FlightBookingWhereUniqueInput uniqueId,
        FlightBookingUpdateInput updateDto
    )
    {
        var flightBooking = updateDto.ToModel(uniqueId);

        _context.Entry(flightBooking).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.FlightBookings.Any(e => e.Id == flightBooking.Id))
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
