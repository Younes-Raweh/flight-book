using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class VatsController : VatsControllerBase
{
    public VatsController(IVatsService service)
        : base(service) { }
}
