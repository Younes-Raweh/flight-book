using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class GalleriesServiceBase : IGalleriesService
{
    protected readonly FlightReservationManagementDbContext _context;

    public GalleriesServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Gallery
    /// </summary>
    public async Task<Gallery> CreateGallery(GalleryCreateInput createDto)
    {
        var gallery = new GalleryDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            gallery.Id = createDto.Id;
        }

        _context.Galleries.Add(gallery);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<GalleryDbModel>(gallery.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Gallery
    /// </summary>
    public async Task DeleteGallery(GalleryWhereUniqueInput uniqueId)
    {
        var gallery = await _context.Galleries.FindAsync(uniqueId.Id);
        if (gallery == null)
        {
            throw new NotFoundException();
        }

        _context.Galleries.Remove(gallery);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Galleries
    /// </summary>
    public async Task<List<Gallery>> Galleries(GalleryFindManyArgs findManyArgs)
    {
        var galleries = await _context
            .Galleries.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return galleries.ConvertAll(gallery => gallery.ToDto());
    }

    /// <summary>
    /// Meta data about Gallery records
    /// </summary>
    public async Task<MetadataDto> GalleriesMeta(GalleryFindManyArgs findManyArgs)
    {
        var count = await _context.Galleries.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Gallery
    /// </summary>
    public async Task<Gallery> Gallery(GalleryWhereUniqueInput uniqueId)
    {
        var galleries = await this.Galleries(
            new GalleryFindManyArgs { Where = new GalleryWhereInput { Id = uniqueId.Id } }
        );
        var gallery = galleries.FirstOrDefault();
        if (gallery == null)
        {
            throw new NotFoundException();
        }

        return gallery;
    }

    /// <summary>
    /// Update one Gallery
    /// </summary>
    public async Task UpdateGallery(GalleryWhereUniqueInput uniqueId, GalleryUpdateInput updateDto)
    {
        var gallery = updateDto.ToModel(uniqueId);

        _context.Entry(gallery).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Galleries.Any(e => e.Id == gallery.Id))
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
