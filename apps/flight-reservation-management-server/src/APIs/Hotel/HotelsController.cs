using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class HotelsController : HotelsControllerBase
{
    public HotelsController(IHotelsService service)
        : base(service) { }
}
