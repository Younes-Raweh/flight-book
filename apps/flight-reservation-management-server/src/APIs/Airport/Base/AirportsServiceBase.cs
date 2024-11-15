using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class AirportsServiceBase : IAirportsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public AirportsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Airport
    /// </summary>
    public async Task<Airport> CreateAirport(AirportCreateInput createDto)
    {
        var airport = new AirportDbModel
        {
            Code = createDto.Code,
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            airport.Id = createDto.Id;
        }

        _context.Airports.Add(airport);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<AirportDbModel>(airport.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Airport
    /// </summary>
    public async Task DeleteAirport(AirportWhereUniqueInput uniqueId)
    {
        var airport = await _context.Airports.FindAsync(uniqueId.Id);
        if (airport == null)
        {
            throw new NotFoundException();
        }

        _context.Airports.Remove(airport);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Airports
    /// </summary>
    public async Task<List<Airport>> Airports(AirportFindManyArgs findManyArgs)
    {
        var airports = await _context
            .Airports.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return airports.ConvertAll(airport => airport.ToDto());
    }

    /// <summary>
    /// Meta data about Airport records
    /// </summary>
    public async Task<MetadataDto> AirportsMeta(AirportFindManyArgs findManyArgs)
    {
        var count = await _context.Airports.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Airport
    /// </summary>
    public async Task<Airport> Airport(AirportWhereUniqueInput uniqueId)
    {
        var airports = await this.Airports(
            new AirportFindManyArgs { Where = new AirportWhereInput { Id = uniqueId.Id } }
        );
        var airport = airports.FirstOrDefault();
        if (airport == null)
        {
            throw new NotFoundException();
        }

        return airport;
    }

    /// <summary>
    /// Update one Airport
    /// </summary>
    public async Task UpdateAirport(AirportWhereUniqueInput uniqueId, AirportUpdateInput updateDto)
    {
        var airport = updateDto.ToModel(uniqueId);

        _context.Entry(airport).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Airports.Any(e => e.Id == airport.Id))
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
