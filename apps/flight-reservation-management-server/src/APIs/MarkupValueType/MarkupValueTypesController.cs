using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class MarkupValueTypesController : MarkupValueTypesControllerBase
{
    public MarkupValueTypesController(IMarkupValueTypesService service)
        : base(service) { }
}
