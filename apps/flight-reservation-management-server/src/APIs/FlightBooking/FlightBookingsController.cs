using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class FlightBookingsController : FlightBookingsControllerBase
{
    public FlightBookingsController(IFlightBookingsService service)
        : base(service) { }
}
