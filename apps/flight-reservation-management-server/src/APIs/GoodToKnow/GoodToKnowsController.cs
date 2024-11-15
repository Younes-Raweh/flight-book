using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class GoodToKnowsController : GoodToKnowsControllerBase
{
    public GoodToKnowsController(IGoodToKnowsService service)
        : base(service) { }
}
