using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class PassengersServiceBase : IPassengersService
{
    protected readonly FlightReservationManagementDbContext _context;

    public PassengersServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Passenger
    /// </summary>
    public async Task<Passenger> CreatePassenger(PassengerCreateInput createDto)
    {
        var passenger = new PassengerDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            passenger.Id = createDto.Id;
        }

        _context.Passengers.Add(passenger);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PassengerDbModel>(passenger.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Passenger
    /// </summary>
    public async Task DeletePassenger(PassengerWhereUniqueInput uniqueId)
    {
        var passenger = await _context.Passengers.FindAsync(uniqueId.Id);
        if (passenger == null)
        {
            throw new NotFoundException();
        }

        _context.Passengers.Remove(passenger);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Passengers
    /// </summary>
    public async Task<List<Passenger>> Passengers(PassengerFindManyArgs findManyArgs)
    {
        var passengers = await _context
            .Passengers.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return passengers.ConvertAll(passenger => passenger.ToDto());
    }

    /// <summary>
    /// Meta data about Passenger records
    /// </summary>
    public async Task<MetadataDto> PassengersMeta(PassengerFindManyArgs findManyArgs)
    {
        var count = await _context.Passengers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Passenger
    /// </summary>
    public async Task<Passenger> Passenger(PassengerWhereUniqueInput uniqueId)
    {
        var passengers = await this.Passengers(
            new PassengerFindManyArgs { Where = new PassengerWhereInput { Id = uniqueId.Id } }
        );
        var passenger = passengers.FirstOrDefault();
        if (passenger == null)
        {
            throw new NotFoundException();
        }

        return passenger;
    }

    /// <summary>
    /// Update one Passenger
    /// </summary>
    public async Task UpdatePassenger(
        PassengerWhereUniqueInput uniqueId,
        PassengerUpdateInput updateDto
    )
    {
        var passenger = updateDto.ToModel(uniqueId);

        _context.Entry(passenger).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Passengers.Any(e => e.Id == passenger.Id))
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
