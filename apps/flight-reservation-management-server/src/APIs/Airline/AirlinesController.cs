using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class AirlinesController : AirlinesControllerBase
{
    public AirlinesController(IAirlinesService service)
        : base(service) { }
}
