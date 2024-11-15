using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class OnlinePaymentsController : OnlinePaymentsControllerBase
{
    public OnlinePaymentsController(IOnlinePaymentsService service)
        : base(service) { }
}
