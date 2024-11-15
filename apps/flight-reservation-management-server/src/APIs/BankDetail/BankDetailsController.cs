using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class BankDetailsController : BankDetailsControllerBase
{
    public BankDetailsController(IBankDetailsService service)
        : base(service) { }
}
