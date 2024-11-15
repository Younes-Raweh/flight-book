using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class MarkdownsController : MarkdownsControllerBase
{
    public MarkdownsController(IMarkdownsService service)
        : base(service) { }
}
