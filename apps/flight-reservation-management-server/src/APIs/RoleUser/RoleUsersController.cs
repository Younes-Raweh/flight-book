using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class RoleUsersController : RoleUsersControllerBase
{
    public RoleUsersController(IRoleUsersService service)
        : base(service) { }
}
