using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class PackageAttractionsExtensions
{
    public static PackageAttraction ToDto(this PackageAttractionDbModel model)
    {
        return new PackageAttraction
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PackageAttractionDbModel ToModel(
        this PackageAttractionUpdateInput updateDto,
        PackageAttractionWhereUniqueInput uniqueId
    )
    {
        var packageAttraction = new PackageAttractionDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            packageAttraction.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            packageAttraction.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return packageAttraction;
    }
}
