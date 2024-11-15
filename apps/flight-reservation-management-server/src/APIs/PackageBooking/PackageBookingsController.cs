using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class PackageBookingsController : PackageBookingsControllerBase
{
    public PackageBookingsController(IPackageBookingsService service)
        : base(service) { }
}
