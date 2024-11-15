using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class NotificationsServiceBase : INotificationsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public NotificationsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Notification
    /// </summary>
    public async Task<Notification> CreateNotification(NotificationCreateInput createDto)
    {
        var notification = new NotificationDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            notification.Id = createDto.Id;
        }

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<NotificationDbModel>(notification.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Notification
    /// </summary>
    public async Task DeleteNotification(NotificationWhereUniqueInput uniqueId)
    {
        var notification = await _context.Notifications.FindAsync(uniqueId.Id);
        if (notification == null)
        {
            throw new NotFoundException();
        }

        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Notifications
    /// </summary>
    public async Task<List<Notification>> Notifications(NotificationFindManyArgs findManyArgs)
    {
        var notifications = await _context
            .Notifications.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return notifications.ConvertAll(notification => notification.ToDto());
    }

    /// <summary>
    /// Meta data about Notification records
    /// </summary>
    public async Task<MetadataDto> NotificationsMeta(NotificationFindManyArgs findManyArgs)
    {
        var count = await _context.Notifications.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Notification
    /// </summary>
    public async Task<Notification> Notification(NotificationWhereUniqueInput uniqueId)
    {
        var notifications = await this.Notifications(
            new NotificationFindManyArgs { Where = new NotificationWhereInput { Id = uniqueId.Id } }
        );
        var notification = notifications.FirstOrDefault();
        if (notification == null)
        {
            throw new NotFoundException();
        }

        return notification;
    }

    /// <summary>
    /// Update one Notification
    /// </summary>
    public async Task UpdateNotification(
        NotificationWhereUniqueInput uniqueId,
        NotificationUpdateInput updateDto
    )
    {
        var notification = updateDto.ToModel(uniqueId);

        _context.Entry(notification).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Notifications.Any(e => e.Id == notification.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
