using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class AirportsExtensions
{
    public static Airport ToDto(this AirportDbModel model)
    {
        return new Airport
        {
            Code = model.Code,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Name = model.Name,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static AirportDbModel ToModel(
        this AirportUpdateInput updateDto,
        AirportWhereUniqueInput uniqueId
    )
    {
        var airport = new AirportDbModel
        {
            Id = uniqueId.Id,
            Code = updateDto.Code,
            Name = updateDto.Name
        };

        if (updateDto.CreatedAt != null)
        {
            airport.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            airport.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return airport;
    }
}
