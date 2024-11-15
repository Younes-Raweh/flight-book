using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class PackageTypesExtensions
{
    public static PackageType ToDto(this PackageTypeDbModel model)
    {
        return new PackageType
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PackageTypeDbModel ToModel(
        this PackageTypeUpdateInput updateDto,
        PackageTypeWhereUniqueInput uniqueId
    )
    {
        var packageType = new PackageTypeDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            packageType.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            packageType.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return packageType;
    }
}
