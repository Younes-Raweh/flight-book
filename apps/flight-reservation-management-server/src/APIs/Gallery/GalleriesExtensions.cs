using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class GalleriesExtensions
{
    public static Gallery ToDto(this GalleryDbModel model)
    {
        return new Gallery
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static GalleryDbModel ToModel(
        this GalleryUpdateInput updateDto,
        GalleryWhereUniqueInput uniqueId
    )
    {
        var gallery = new GalleryDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            gallery.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            gallery.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return gallery;
    }
}
