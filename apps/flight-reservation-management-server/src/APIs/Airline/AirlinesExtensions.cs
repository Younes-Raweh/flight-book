using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class AirlinesExtensions
{
    public static Airline ToDto(this AirlineDbModel model)
    {
        return new Airline
        {
            Code = model.Code,
            CreatedAt = model.CreatedAt,
            IcaoCode = model.IcaoCode,
            Id = model.Id,
            Name = model.Name,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static AirlineDbModel ToModel(
        this AirlineUpdateInput updateDto,
        AirlineWhereUniqueInput uniqueId
    )
    {
        var airline = new AirlineDbModel
        {
            Id = uniqueId.Id,
            Code = updateDto.Code,
            IcaoCode = updateDto.IcaoCode,
            Name = updateDto.Name
        };

        if (updateDto.CreatedAt != null)
        {
            airline.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            airline.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return airline;
    }
}
