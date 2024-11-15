using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class EmailSubscribersController : EmailSubscribersControllerBase
{
    public EmailSubscribersController(IEmailSubscribersService service)
        : base(service) { }
}
