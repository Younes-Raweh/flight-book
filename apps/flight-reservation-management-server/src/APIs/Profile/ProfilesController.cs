using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class ProfilesController : ProfilesControllerBase
{
    public ProfilesController(IProfilesService service)
        : base(service) { }
}
