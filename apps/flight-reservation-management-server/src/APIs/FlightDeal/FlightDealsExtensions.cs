using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class FlightDealsExtensions
{
    public static FlightDeal ToDto(this FlightDealDbModel model)
    {
        return new FlightDeal
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static FlightDealDbModel ToModel(
        this FlightDealUpdateInput updateDto,
        FlightDealWhereUniqueInput uniqueId
    )
    {
        var flightDeal = new FlightDealDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            flightDeal.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            flightDeal.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return flightDeal;
    }
}
