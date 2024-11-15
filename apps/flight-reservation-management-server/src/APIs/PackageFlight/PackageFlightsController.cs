using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class PackageFlightsController : PackageFlightsControllerBase
{
    public PackageFlightsController(IPackageFlightsService service)
        : base(service) { }
}
