using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class PackageTypesController : PackageTypesControllerBase
{
    public PackageTypesController(IPackageTypesService service)
        : base(service) { }
}
