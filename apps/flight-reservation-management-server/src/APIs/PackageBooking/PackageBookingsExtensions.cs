using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class PackageBookingsExtensions
{
    public static PackageBooking ToDto(this PackageBookingDbModel model)
    {
        return new PackageBooking
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PackageBookingDbModel ToModel(
        this PackageBookingUpdateInput updateDto,
        PackageBookingWhereUniqueInput uniqueId
    )
    {
        var packageBooking = new PackageBookingDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            packageBooking.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            packageBooking.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return packageBooking;
    }
}
