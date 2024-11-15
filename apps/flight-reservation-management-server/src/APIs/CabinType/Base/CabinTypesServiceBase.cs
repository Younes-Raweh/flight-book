using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class CabinTypesServiceBase : ICabinTypesService
{
    protected readonly FlightReservationManagementDbContext _context;

    public CabinTypesServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one CabinType
    /// </summary>
    public async Task<CabinType> CreateCabinType(CabinTypeCreateInput createDto)
    {
        var cabinType = new CabinTypeDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            cabinType.Id = createDto.Id;
        }

        _context.CabinTypes.Add(cabinType);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CabinTypeDbModel>(cabinType.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one CabinType
    /// </summary>
    public async Task DeleteCabinType(CabinTypeWhereUniqueInput uniqueId)
    {
        var cabinType = await _context.CabinTypes.FindAsync(uniqueId.Id);
        if (cabinType == null)
        {
            throw new NotFoundException();
        }

        _context.CabinTypes.Remove(cabinType);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many CabinTypes
    /// </summary>
    public async Task<List<CabinType>> CabinTypes(CabinTypeFindManyArgs findManyArgs)
    {
        var cabinTypes = await _context
            .CabinTypes.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return cabinTypes.ConvertAll(cabinType => cabinType.ToDto());
    }

    /// <summary>
    /// Meta data about CabinType records
    /// </summary>
    public async Task<MetadataDto> CabinTypesMeta(CabinTypeFindManyArgs findManyArgs)
    {
        var count = await _context.CabinTypes.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one CabinType
    /// </summary>
    public async Task<CabinType> CabinType(CabinTypeWhereUniqueInput uniqueId)
    {
        var cabinTypes = await this.CabinTypes(
            new CabinTypeFindManyArgs { Where = new CabinTypeWhereInput { Id = uniqueId.Id } }
        );
        var cabinType = cabinTypes.FirstOrDefault();
        if (cabinType == null)
        {
            throw new NotFoundException();
        }

        return cabinType;
    }

    /// <summary>
    /// Update one CabinType
    /// </summary>
    public async Task UpdateCabinType(
        CabinTypeWhereUniqueInput uniqueId,
        CabinTypeUpdateInput updateDto
    )
    {
        var cabinType = updateDto.ToModel(uniqueId);

        _context.Entry(cabinType).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.CabinTypes.Any(e => e.Id == cabinType.Id))
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
