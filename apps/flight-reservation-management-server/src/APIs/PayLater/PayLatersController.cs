using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class PayLatersController : PayLatersControllerBase
{
    public PayLatersController(IPayLatersService service)
        : base(service) { }
}
