using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class VisaApplicationsController : VisaApplicationsControllerBase
{
    public VisaApplicationsController(IVisaApplicationsService service)
        : base(service) { }
}
