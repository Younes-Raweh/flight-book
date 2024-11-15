using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class HotelDealsController : HotelDealsControllerBase
{
    public HotelDealsController(IHotelDealsService service)
        : base(service) { }
}
