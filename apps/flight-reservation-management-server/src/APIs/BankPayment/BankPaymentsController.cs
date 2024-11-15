using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class BankPaymentsController : BankPaymentsControllerBase
{
    public BankPaymentsController(IBankPaymentsService service)
        : base(service) { }
}
