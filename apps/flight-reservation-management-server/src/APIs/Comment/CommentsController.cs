using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class CommentsController : CommentsControllerBase
{
    public CommentsController(ICommentsService service)
        : base(service) { }
}
