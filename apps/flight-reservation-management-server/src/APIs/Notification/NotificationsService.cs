using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class NotificationsService : NotificationsServiceBase
{
    public NotificationsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
