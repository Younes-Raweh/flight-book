using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class MarkdownsServiceBase : IMarkdownsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public MarkdownsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Markdown
    /// </summary>
    public async Task<Markdown> CreateMarkdown(MarkdownCreateInput createDto)
    {
        var markdown = new MarkdownDbModel
        {
            AirlineCode = createDto.AirlineCode,
            CreatedAt = createDto.CreatedAt,
            TypeField = createDto.TypeField,
            UpdatedAt = createDto.UpdatedAt,
            Value = createDto.Value
        };

        if (createDto.Id != null)
        {
            markdown.Id = createDto.Id;
        }

        _context.Markdowns.Add(markdown);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<MarkdownDbModel>(markdown.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Markdown
    /// </summary>
    public async Task DeleteMarkdown(MarkdownWhereUniqueInput uniqueId)
    {
        var markdown = await _context.Markdowns.FindAsync(uniqueId.Id);
        if (markdown == null)
        {
            throw new NotFoundException();
        }

        _context.Markdowns.Remove(markdown);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Markdowns
    /// </summary>
    public async Task<List<Markdown>> Markdowns(MarkdownFindManyArgs findManyArgs)
    {
        var markdowns = await _context
            .Markdowns.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return markdowns.ConvertAll(markdown => markdown.ToDto());
    }

    /// <summary>
    /// Meta data about Markdown records
    /// </summary>
    public async Task<MetadataDto> MarkdownsMeta(MarkdownFindManyArgs findManyArgs)
    {
        var count = await _context.Markdowns.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Markdown
    /// </summary>
    public async Task<Markdown> Markdown(MarkdownWhereUniqueInput uniqueId)
    {
        var markdowns = await this.Markdowns(
            new MarkdownFindManyArgs { Where = new MarkdownWhereInput { Id = uniqueId.Id } }
        );
        var markdown = markdowns.FirstOrDefault();
        if (markdown == null)
        {
            throw new NotFoundException();
        }

        return markdown;
    }

    /// <summary>
    /// Update one Markdown
    /// </summary>
    public async Task UpdateMarkdown(
        MarkdownWhereUniqueInput uniqueId,
        MarkdownUpdateInput updateDto
    )
    {
        var markdown = updateDto.ToModel(uniqueId);

        _context.Entry(markdown).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Markdowns.Any(e => e.Id == markdown.Id))
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
