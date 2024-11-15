using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class PackageHotelsController : PackageHotelsControllerBase
{
    public PackageHotelsController(IPackageHotelsService service)
        : base(service) { }
}
