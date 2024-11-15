using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class AirlinesServiceBase : IAirlinesService
{
    protected readonly FlightReservationManagementDbContext _context;

    public AirlinesServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Airline
    /// </summary>
    public async Task<Airline> CreateAirline(AirlineCreateInput createDto)
    {
        var airline = new AirlineDbModel
        {
            Code = createDto.Code,
            CreatedAt = createDto.CreatedAt,
            IcaoCode = createDto.IcaoCode,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            airline.Id = createDto.Id;
        }

        _context.Airlines.Add(airline);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<AirlineDbModel>(airline.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Airline
    /// </summary>
    public async Task DeleteAirline(AirlineWhereUniqueInput uniqueId)
    {
        var airline = await _context.Airlines.FindAsync(uniqueId.Id);
        if (airline == null)
        {
            throw new NotFoundException();
        }

        _context.Airlines.Remove(airline);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Airlines
    /// </summary>
    public async Task<List<Airline>> Airlines(AirlineFindManyArgs findManyArgs)
    {
        var airlines = await _context
            .Airlines.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return airlines.ConvertAll(airline => airline.ToDto());
    }

    /// <summary>
    /// Meta data about Airline records
    /// </summary>
    public async Task<MetadataDto> AirlinesMeta(AirlineFindManyArgs findManyArgs)
    {
        var count = await _context.Airlines.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Airline
    /// </summary>
    public async Task<Airline> Airline(AirlineWhereUniqueInput uniqueId)
    {
        var airlines = await this.Airlines(
            new AirlineFindManyArgs { Where = new AirlineWhereInput { Id = uniqueId.Id } }
        );
        var airline = airlines.FirstOrDefault();
        if (airline == null)
        {
            throw new NotFoundException();
        }

        return airline;
    }

    /// <summary>
    /// Update one Airline
    /// </summary>
    public async Task UpdateAirline(AirlineWhereUniqueInput uniqueId, AirlineUpdateInput updateDto)
    {
        var airline = updateDto.ToModel(uniqueId);

        _context.Entry(airline).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Airlines.Any(e => e.Id == airline.Id))
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
