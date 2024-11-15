using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class BanksController : BanksControllerBase
{
    public BanksController(IBanksService service)
        : base(service) { }
}
