using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class PackageFlightsExtensions
{
    public static PackageFlight ToDto(this PackageFlightDbModel model)
    {
        return new PackageFlight
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PackageFlightDbModel ToModel(
        this PackageFlightUpdateInput updateDto,
        PackageFlightWhereUniqueInput uniqueId
    )
    {
        var packageFlight = new PackageFlightDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            packageFlight.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            packageFlight.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return packageFlight;
    }
}
