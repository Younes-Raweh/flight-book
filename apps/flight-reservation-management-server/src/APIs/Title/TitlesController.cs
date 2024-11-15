using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class TitlesController : TitlesControllerBase
{
    public TitlesController(ITitlesService service)
        : base(service) { }
}
