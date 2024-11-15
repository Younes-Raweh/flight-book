using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class CarBookingsController : CarBookingsControllerBase
{
    public CarBookingsController(ICarBookingsService service)
        : base(service) { }
}
