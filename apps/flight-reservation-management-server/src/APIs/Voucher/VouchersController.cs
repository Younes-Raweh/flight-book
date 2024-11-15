using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class VouchersController : VouchersControllerBase
{
    public VouchersController(IVouchersService service)
        : base(service) { }
}
