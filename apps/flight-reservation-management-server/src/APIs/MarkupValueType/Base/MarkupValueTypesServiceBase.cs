using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class MarkupValueTypesServiceBase : IMarkupValueTypesService
{
    protected readonly FlightReservationManagementDbContext _context;

    public MarkupValueTypesServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one MarkupValueType
    /// </summary>
    public async Task<MarkupValueType> CreateMarkupValueType(MarkupValueTypeCreateInput createDto)
    {
        var markupValueType = new MarkupValueTypeDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            markupValueType.Id = createDto.Id;
        }

        _context.MarkupValueTypes.Add(markupValueType);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<MarkupValueTypeDbModel>(markupValueType.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one MarkupValueType
    /// </summary>
    public async Task DeleteMarkupValueType(MarkupValueTypeWhereUniqueInput uniqueId)
    {
        var markupValueType = await _context.MarkupValueTypes.FindAsync(uniqueId.Id);
        if (markupValueType == null)
        {
            throw new NotFoundException();
        }

        _context.MarkupValueTypes.Remove(markupValueType);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many MarkupValueTypes
    /// </summary>
    public async Task<List<MarkupValueType>> MarkupValueTypes(
        MarkupValueTypeFindManyArgs findManyArgs
    )
    {
        var markupValueTypes = await _context
            .MarkupValueTypes.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return markupValueTypes.ConvertAll(markupValueType => markupValueType.ToDto());
    }

    /// <summary>
    /// Meta data about MarkupValueType records
    /// </summary>
    public async Task<MetadataDto> MarkupValueTypesMeta(MarkupValueTypeFindManyArgs findManyArgs)
    {
        var count = await _context.MarkupValueTypes.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one MarkupValueType
    /// </summary>
    public async Task<MarkupValueType> MarkupValueType(MarkupValueTypeWhereUniqueInput uniqueId)
    {
        var markupValueTypes = await this.MarkupValueTypes(
            new MarkupValueTypeFindManyArgs
            {
                Where = new MarkupValueTypeWhereInput { Id = uniqueId.Id }
            }
        );
        var markupValueType = markupValueTypes.FirstOrDefault();
        if (markupValueType == null)
        {
            throw new NotFoundException();
        }

        return markupValueType;
    }

    /// <summary>
    /// Update one MarkupValueType
    /// </summary>
    public async Task UpdateMarkupValueType(
        MarkupValueTypeWhereUniqueInput uniqueId,
        MarkupValueTypeUpdateInput updateDto
    )
    {
        var markupValueType = updateDto.ToModel(uniqueId);

        _context.Entry(markupValueType).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.MarkupValueTypes.Any(e => e.Id == markupValueType.Id))
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
