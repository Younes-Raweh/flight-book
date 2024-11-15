using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class PackageCategoriesExtensions
{
    public static PackageCategory ToDto(this PackageCategoryDbModel model)
    {
        return new PackageCategory
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PackageCategoryDbModel ToModel(
        this PackageCategoryUpdateInput updateDto,
        PackageCategoryWhereUniqueInput uniqueId
    )
    {
        var packageCategory = new PackageCategoryDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            packageCategory.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            packageCategory.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return packageCategory;
    }
}
