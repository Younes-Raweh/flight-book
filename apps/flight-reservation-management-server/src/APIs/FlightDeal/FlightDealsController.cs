using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class FlightDealsController : FlightDealsControllerBase
{
    public FlightDealsController(IFlightDealsService service)
        : base(service) { }
}
