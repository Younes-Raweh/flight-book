using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class FlightsController : FlightsControllerBase
{
    public FlightsController(IFlightsService service)
        : base(service) { }
}
