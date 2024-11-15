using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class CabinTypesExtensions
{
    public static CabinType ToDto(this CabinTypeDbModel model)
    {
        return new CabinType
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static CabinTypeDbModel ToModel(
        this CabinTypeUpdateInput updateDto,
        CabinTypeWhereUniqueInput uniqueId
    )
    {
        var cabinType = new CabinTypeDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            cabinType.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            cabinType.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return cabinType;
    }
}
