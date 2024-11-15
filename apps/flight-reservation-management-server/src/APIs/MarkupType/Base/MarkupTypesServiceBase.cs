using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class MarkupTypesServiceBase : IMarkupTypesService
{
    protected readonly FlightReservationManagementDbContext _context;

    public MarkupTypesServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one MarkupType
    /// </summary>
    public async Task<MarkupType> CreateMarkupType(MarkupTypeCreateInput createDto)
    {
        var markupType = new MarkupTypeDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            markupType.Id = createDto.Id;
        }

        _context.MarkupTypes.Add(markupType);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<MarkupTypeDbModel>(markupType.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one MarkupType
    /// </summary>
    public async Task DeleteMarkupType(MarkupTypeWhereUniqueInput uniqueId)
    {
        var markupType = await _context.MarkupTypes.FindAsync(uniqueId.Id);
        if (markupType == null)
        {
            throw new NotFoundException();
        }

        _context.MarkupTypes.Remove(markupType);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many MarkupTypes
    /// </summary>
    public async Task<List<MarkupType>> MarkupTypes(MarkupTypeFindManyArgs findManyArgs)
    {
        var markupTypes = await _context
            .MarkupTypes.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return markupTypes.ConvertAll(markupType => markupType.ToDto());
    }

    /// <summary>
    /// Meta data about MarkupType records
    /// </summary>
    public async Task<MetadataDto> MarkupTypesMeta(MarkupTypeFindManyArgs findManyArgs)
    {
        var count = await _context.MarkupTypes.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one MarkupType
    /// </summary>
    public async Task<MarkupType> MarkupType(MarkupTypeWhereUniqueInput uniqueId)
    {
        var markupTypes = await this.MarkupTypes(
            new MarkupTypeFindManyArgs { Where = new MarkupTypeWhereInput { Id = uniqueId.Id } }
        );
        var markupType = markupTypes.FirstOrDefault();
        if (markupType == null)
        {
            throw new NotFoundException();
        }

        return markupType;
    }

    /// <summary>
    /// Update one MarkupType
    /// </summary>
    public async Task UpdateMarkupType(
        MarkupTypeWhereUniqueInput uniqueId,
        MarkupTypeUpdateInput updateDto
    )
    {
        var markupType = updateDto.ToModel(uniqueId);

        _context.Entry(markupType).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.MarkupTypes.Any(e => e.Id == markupType.Id))
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
