using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class InvoicesController : InvoicesControllerBase
{
    public InvoicesController(IInvoicesService service)
        : base(service) { }
}
