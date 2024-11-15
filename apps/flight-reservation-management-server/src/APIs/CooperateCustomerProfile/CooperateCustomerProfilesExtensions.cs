using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class CooperateCustomerProfilesExtensions
{
    public static CooperateCustomerProfile ToDto(this CooperateCustomerProfileDbModel model)
    {
        return new CooperateCustomerProfile
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static CooperateCustomerProfileDbModel ToModel(
        this CooperateCustomerProfileUpdateInput updateDto,
        CooperateCustomerProfileWhereUniqueInput uniqueId
    )
    {
        var cooperateCustomerProfile = new CooperateCustomerProfileDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            cooperateCustomerProfile.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            cooperateCustomerProfile.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return cooperateCustomerProfile;
    }
}
