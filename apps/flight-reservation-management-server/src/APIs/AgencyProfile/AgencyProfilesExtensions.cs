using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class AgencyProfilesExtensions
{
    public static AgencyProfile ToDto(this AgencyProfileDbModel model)
    {
        return new AgencyProfile
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static AgencyProfileDbModel ToModel(
        this AgencyProfileUpdateInput updateDto,
        AgencyProfileWhereUniqueInput uniqueId
    )
    {
        var agencyProfile = new AgencyProfileDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            agencyProfile.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            agencyProfile.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return agencyProfile;
    }
}
