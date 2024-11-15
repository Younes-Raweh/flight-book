using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class TourOperatorsServiceBase : ITourOperatorsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public TourOperatorsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Tour Operator
    /// </summary>
    public async Task<TourOperator> CreateTourOperator(TourOperatorCreateInput createDto)
    {
        var tourOperator = new TourOperatorDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            tourOperator.Id = createDto.Id;
        }

        _context.TourOperators.Add(tourOperator);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<TourOperatorDbModel>(tourOperator.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Tour Operator
    /// </summary>
    public async Task DeleteTourOperator(TourOperatorWhereUniqueInput uniqueId)
    {
        var tourOperator = await _context.TourOperators.FindAsync(uniqueId.Id);
        if (tourOperator == null)
        {
            throw new NotFoundException();
        }

        _context.TourOperators.Remove(tourOperator);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Tour Operators
    /// </summary>
    public async Task<List<TourOperator>> TourOperators(TourOperatorFindManyArgs findManyArgs)
    {
        var tourOperators = await _context
            .TourOperators.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return tourOperators.ConvertAll(tourOperator => tourOperator.ToDto());
    }

    /// <summary>
    /// Meta data about Tour Operator records
    /// </summary>
    public async Task<MetadataDto> TourOperatorsMeta(TourOperatorFindManyArgs findManyArgs)
    {
        var count = await _context.TourOperators.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Tour Operator
    /// </summary>
    public async Task<TourOperator> TourOperator(TourOperatorWhereUniqueInput uniqueId)
    {
        var tourOperators = await this.TourOperators(
            new TourOperatorFindManyArgs { Where = new TourOperatorWhereInput { Id = uniqueId.Id } }
        );
        var tourOperator = tourOperators.FirstOrDefault();
        if (tourOperator == null)
        {
            throw new NotFoundException();
        }

        return tourOperator;
    }

    /// <summary>
    /// Update one Tour Operator
    /// </summary>
    public async Task UpdateTourOperator(
        TourOperatorWhereUniqueInput uniqueId,
        TourOperatorUpdateInput updateDto
    )
    {
        var tourOperator = updateDto.ToModel(uniqueId);

        _context.Entry(tourOperator).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.TourOperators.Any(e => e.Id == tourOperator.Id))
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
