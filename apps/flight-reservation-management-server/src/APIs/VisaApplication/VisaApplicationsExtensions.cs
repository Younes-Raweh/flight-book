using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class VisaApplicationsExtensions
{
    public static VisaApplication ToDto(this VisaApplicationDbModel model)
    {
        return new VisaApplication
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static VisaApplicationDbModel ToModel(
        this VisaApplicationUpdateInput updateDto,
        VisaApplicationWhereUniqueInput uniqueId
    )
    {
        var visaApplication = new VisaApplicationDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            visaApplication.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            visaApplication.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return visaApplication;
    }
}
