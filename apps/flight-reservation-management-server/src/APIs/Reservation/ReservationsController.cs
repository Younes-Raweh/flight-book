using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class ReservationsController : ReservationsControllerBase
{
    public ReservationsController(IReservationsService service)
        : base(service) { }
}
