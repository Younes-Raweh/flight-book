using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class NotificationsExtensions
{
    public static Notification ToDto(this NotificationDbModel model)
    {
        return new Notification
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static NotificationDbModel ToModel(
        this NotificationUpdateInput updateDto,
        NotificationWhereUniqueInput uniqueId
    )
    {
        var notification = new NotificationDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            notification.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            notification.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return notification;
    }
}
