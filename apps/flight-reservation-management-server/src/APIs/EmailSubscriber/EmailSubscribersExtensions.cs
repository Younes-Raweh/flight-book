using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class EmailSubscribersExtensions
{
    public static EmailSubscriber ToDto(this EmailSubscriberDbModel model)
    {
        return new EmailSubscriber
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static EmailSubscriberDbModel ToModel(
        this EmailSubscriberUpdateInput updateDto,
        EmailSubscriberWhereUniqueInput uniqueId
    )
    {
        var emailSubscriber = new EmailSubscriberDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            emailSubscriber.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            emailSubscriber.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return emailSubscriber;
    }
}
