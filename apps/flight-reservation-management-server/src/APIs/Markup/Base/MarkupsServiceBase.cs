using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class MarkupsServiceBase : IMarkupsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public MarkupsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Markup
    /// </summary>
    public async Task<Markup> CreateMarkup(MarkupCreateInput createDto)
    {
        var markup = new MarkupDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            markup.Id = createDto.Id;
        }

        _context.Markups.Add(markup);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<MarkupDbModel>(markup.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Markup
    /// </summary>
    public async Task DeleteMarkup(MarkupWhereUniqueInput uniqueId)
    {
        var markup = await _context.Markups.FindAsync(uniqueId.Id);
        if (markup == null)
        {
            throw new NotFoundException();
        }

        _context.Markups.Remove(markup);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Markups
    /// </summary>
    public async Task<List<Markup>> Markups(MarkupFindManyArgs findManyArgs)
    {
        var markups = await _context
            .Markups.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return markups.ConvertAll(markup => markup.ToDto());
    }

    /// <summary>
    /// Meta data about Markup records
    /// </summary>
    public async Task<MetadataDto> MarkupsMeta(MarkupFindManyArgs findManyArgs)
    {
        var count = await _context.Markups.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Markup
    /// </summary>
    public async Task<Markup> Markup(MarkupWhereUniqueInput uniqueId)
    {
        var markups = await this.Markups(
            new MarkupFindManyArgs { Where = new MarkupWhereInput { Id = uniqueId.Id } }
        );
        var markup = markups.FirstOrDefault();
        if (markup == null)
        {
            throw new NotFoundException();
        }

        return markup;
    }

    /// <summary>
    /// Update one Markup
    /// </summary>
    public async Task UpdateMarkup(MarkupWhereUniqueInput uniqueId, MarkupUpdateInput updateDto)
    {
        var markup = updateDto.ToModel(uniqueId);

        _context.Entry(markup).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Markups.Any(e => e.Id == markup.Id))
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
