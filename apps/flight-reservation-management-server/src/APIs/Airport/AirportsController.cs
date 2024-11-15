using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class AirportsController : AirportsControllerBase
{
    public AirportsController(IAirportsService service)
        : base(service) { }
}
