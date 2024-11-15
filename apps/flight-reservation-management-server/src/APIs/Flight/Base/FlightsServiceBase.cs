using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class FlightsServiceBase : IFlightsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public FlightsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Flight
    /// </summary>
    public async Task<Flight> CreateFlight(FlightCreateInput createDto)
    {
        var flight = new FlightDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            flight.Id = createDto.Id;
        }

        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<FlightDbModel>(flight.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Flight
    /// </summary>
    public async Task DeleteFlight(FlightWhereUniqueInput uniqueId)
    {
        var flight = await _context.Flights.FindAsync(uniqueId.Id);
        if (flight == null)
        {
            throw new NotFoundException();
        }

        _context.Flights.Remove(flight);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Flights
    /// </summary>
    public async Task<List<Flight>> Flights(FlightFindManyArgs findManyArgs)
    {
        var flights = await _context
            .Flights.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return flights.ConvertAll(flight => flight.ToDto());
    }

    /// <summary>
    /// Meta data about Flight records
    /// </summary>
    public async Task<MetadataDto> FlightsMeta(FlightFindManyArgs findManyArgs)
    {
        var count = await _context.Flights.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Flight
    /// </summary>
    public async Task<Flight> Flight(FlightWhereUniqueInput uniqueId)
    {
        var flights = await this.Flights(
            new FlightFindManyArgs { Where = new FlightWhereInput { Id = uniqueId.Id } }
        );
        var flight = flights.FirstOrDefault();
        if (flight == null)
        {
            throw new NotFoundException();
        }

        return flight;
    }

    /// <summary>
    /// Update one Flight
    /// </summary>
    public async Task UpdateFlight(FlightWhereUniqueInput uniqueId, FlightUpdateInput updateDto)
    {
        var flight = updateDto.ToModel(uniqueId);

        _context.Entry(flight).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Flights.Any(e => e.Id == flight.Id))
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
