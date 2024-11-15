using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class AgencyProfilesController : AgencyProfilesControllerBase
{
    public AgencyProfilesController(IAgencyProfilesService service)
        : base(service) { }
}
