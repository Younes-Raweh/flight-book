using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class PackageModelsController : PackageModelsControllerBase
{
    public PackageModelsController(IPackageModelsService service)
        : base(service) { }
}
