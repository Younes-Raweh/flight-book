using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class MarkupsController : MarkupsControllerBase
{
    public MarkupsController(IMarkupsService service)
        : base(service) { }
}
