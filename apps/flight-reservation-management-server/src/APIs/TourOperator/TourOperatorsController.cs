using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class TourOperatorsController : TourOperatorsControllerBase
{
    public TourOperatorsController(ITourOperatorsService service)
        : base(service) { }
}
