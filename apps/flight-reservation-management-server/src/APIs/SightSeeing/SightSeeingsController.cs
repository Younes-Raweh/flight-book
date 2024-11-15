using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class SightSeeingsController : SightSeeingsControllerBase
{
    public SightSeeingsController(ISightSeeingsService service)
        : base(service) { }
}
