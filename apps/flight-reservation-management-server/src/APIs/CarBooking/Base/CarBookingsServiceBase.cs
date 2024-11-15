using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class CarBookingsServiceBase : ICarBookingsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public CarBookingsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one CarBooking
    /// </summary>
    public async Task<CarBooking> CreateCarBooking(CarBookingCreateInput createDto)
    {
        var carBooking = new CarBookingDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            carBooking.Id = createDto.Id;
        }

        _context.CarBookings.Add(carBooking);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CarBookingDbModel>(carBooking.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one CarBooking
    /// </summary>
    public async Task DeleteCarBooking(CarBookingWhereUniqueInput uniqueId)
    {
        var carBooking = await _context.CarBookings.FindAsync(uniqueId.Id);
        if (carBooking == null)
        {
            throw new NotFoundException();
        }

        _context.CarBookings.Remove(carBooking);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many CarBookings
    /// </summary>
    public async Task<List<CarBooking>> CarBookings(CarBookingFindManyArgs findManyArgs)
    {
        var carBookings = await _context
            .CarBookings.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return carBookings.ConvertAll(carBooking => carBooking.ToDto());
    }

    /// <summary>
    /// Meta data about CarBooking records
    /// </summary>
    public async Task<MetadataDto> CarBookingsMeta(CarBookingFindManyArgs findManyArgs)
    {
        var count = await _context.CarBookings.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one CarBooking
    /// </summary>
    public async Task<CarBooking> CarBooking(CarBookingWhereUniqueInput uniqueId)
    {
        var carBookings = await this.CarBookings(
            new CarBookingFindManyArgs { Where = new CarBookingWhereInput { Id = uniqueId.Id } }
        );
        var carBooking = carBookings.FirstOrDefault();
        if (carBooking == null)
        {
            throw new NotFoundException();
        }

        return carBooking;
    }

    /// <summary>
    /// Update one CarBooking
    /// </summary>
    public async Task UpdateCarBooking(
        CarBookingWhereUniqueInput uniqueId,
        CarBookingUpdateInput updateDto
    )
    {
        var carBooking = updateDto.ToModel(uniqueId);

        _context.Entry(carBooking).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.CarBookings.Any(e => e.Id == carBooking.Id))
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
