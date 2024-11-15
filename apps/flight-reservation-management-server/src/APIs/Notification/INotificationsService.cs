using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface INotificationsService
{
    /// <summary>
    /// Create one Notification
    /// </summary>
    public Task<Notification> CreateNotification(NotificationCreateInput notification);

    /// <summary>
    /// Delete one Notification
    /// </summary>
    public Task DeleteNotification(NotificationWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Notifications
    /// </summary>
    public Task<List<Notification>> Notifications(NotificationFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Notification records
    /// </summary>
    public Task<MetadataDto> NotificationsMeta(NotificationFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Notification
    /// </summary>
    public Task<Notification> Notification(NotificationWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Notification
    /// </summary>
    public Task UpdateNotification(
        NotificationWhereUniqueInput uniqueId,
        NotificationUpdateInput updateDto
    );
}
