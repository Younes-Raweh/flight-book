using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class MarkupTypesController : MarkupTypesControllerBase
{
    public MarkupTypesController(IMarkupTypesService service)
        : base(service) { }
}
