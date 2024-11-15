using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class PassengersController : PassengersControllerBase
{
    public PassengersController(IPassengersService service)
        : base(service) { }
}
