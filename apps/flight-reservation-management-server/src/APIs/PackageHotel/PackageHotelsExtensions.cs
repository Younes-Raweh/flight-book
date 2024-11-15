using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class PackageHotelsExtensions
{
    public static PackageHotel ToDto(this PackageHotelDbModel model)
    {
        return new PackageHotel
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PackageHotelDbModel ToModel(
        this PackageHotelUpdateInput updateDto,
        PackageHotelWhereUniqueInput uniqueId
    )
    {
        var packageHotel = new PackageHotelDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            packageHotel.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            packageHotel.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return packageHotel;
    }
}
