using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class PackageAttractionsController : PackageAttractionsControllerBase
{
    public PackageAttractionsController(IPackageAttractionsService service)
        : base(service) { }
}
