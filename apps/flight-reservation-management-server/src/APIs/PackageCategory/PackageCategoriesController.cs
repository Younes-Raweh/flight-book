using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class PackageCategoriesController : PackageCategoriesControllerBase
{
    public PackageCategoriesController(IPackageCategoriesService service)
        : base(service) { }
}
