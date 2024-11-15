using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class RolesController : RolesControllerBase
{
    public RolesController(IRolesService service)
        : base(service) { }
}
