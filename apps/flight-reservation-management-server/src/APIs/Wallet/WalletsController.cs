using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class WalletsController : WalletsControllerBase
{
    public WalletsController(IWalletsService service)
        : base(service) { }
}
