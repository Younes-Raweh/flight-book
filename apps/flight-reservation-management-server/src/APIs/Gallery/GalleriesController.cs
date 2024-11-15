using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class GalleriesController : GalleriesControllerBase
{
    public GalleriesController(IGalleriesService service)
        : base(service) { }
}
