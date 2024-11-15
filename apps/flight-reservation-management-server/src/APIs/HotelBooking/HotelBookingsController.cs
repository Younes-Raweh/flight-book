using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class HotelBookingsController : HotelBookingsControllerBase
{
    public HotelBookingsController(IHotelBookingsService service)
        : base(service) { }
}
