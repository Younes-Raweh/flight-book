using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class GendersController : GendersControllerBase
{
    public GendersController(IGendersService service)
        : base(service) { }
}
