using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class TitlesServiceBase : ITitlesService
{
    protected readonly FlightReservationManagementDbContext _context;

    public TitlesServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Title
    /// </summary>
    public async Task<Title> CreateTitle(TitleCreateInput createDto)
    {
        var title = new TitleDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            title.Id = createDto.Id;
        }

        _context.Titles.Add(title);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<TitleDbModel>(title.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Title
    /// </summary>
    public async Task DeleteTitle(TitleWhereUniqueInput uniqueId)
    {
        var title = await _context.Titles.FindAsync(uniqueId.Id);
        if (title == null)
        {
            throw new NotFoundException();
        }

        _context.Titles.Remove(title);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Titles
    /// </summary>
    public async Task<List<Title>> Titles(TitleFindManyArgs findManyArgs)
    {
        var titles = await _context
            .Titles.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return titles.ConvertAll(title => title.ToDto());
    }

    /// <summary>
    /// Meta data about Title records
    /// </summary>
    public async Task<MetadataDto> TitlesMeta(TitleFindManyArgs findManyArgs)
    {
        var count = await _context.Titles.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Title
    /// </summary>
    public async Task<Title> Title(TitleWhereUniqueInput uniqueId)
    {
        var titles = await this.Titles(
            new TitleFindManyArgs { Where = new TitleWhereInput { Id = uniqueId.Id } }
        );
        var title = titles.FirstOrDefault();
        if (title == null)
        {
            throw new NotFoundException();
        }

        return title;
    }

    /// <summary>
    /// Update one Title
    /// </summary>
    public async Task UpdateTitle(TitleWhereUniqueInput uniqueId, TitleUpdateInput updateDto)
    {
        var title = updateDto.ToModel(uniqueId);

        _context.Entry(title).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Titles.Any(e => e.Id == title.Id))
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
