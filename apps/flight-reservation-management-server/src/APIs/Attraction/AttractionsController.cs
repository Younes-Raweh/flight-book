using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class AttractionsController : AttractionsControllerBase
{
    public AttractionsController(IAttractionsService service)
        : base(service) { }
}
