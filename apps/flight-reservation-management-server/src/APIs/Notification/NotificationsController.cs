using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class NotificationsController : NotificationsControllerBase
{
    public NotificationsController(INotificationsService service)
        : base(service) { }
}
