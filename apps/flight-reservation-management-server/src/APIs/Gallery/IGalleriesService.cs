using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IGalleriesService
{
    /// <summary>
    /// Create one Gallery
    /// </summary>
    public Task<Gallery> CreateGallery(GalleryCreateInput gallery);

    /// <summary>
    /// Delete one Gallery
    /// </summary>
    public Task DeleteGallery(GalleryWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Galleries
    /// </summary>
    public Task<List<Gallery>> Galleries(GalleryFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Gallery records
    /// </summary>
    public Task<MetadataDto> GalleriesMeta(GalleryFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Gallery
    /// </summary>
    public Task<Gallery> Gallery(GalleryWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Gallery
    /// </summary>
    public Task UpdateGallery(GalleryWhereUniqueInput uniqueId, GalleryUpdateInput updateDto);
}
