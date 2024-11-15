using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class TravelPackagesController : TravelPackagesControllerBase
{
    public TravelPackagesController(ITravelPackagesService service)
        : base(service) { }
}
