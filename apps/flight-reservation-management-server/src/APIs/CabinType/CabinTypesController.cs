using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class CabinTypesController : CabinTypesControllerBase
{
    public CabinTypesController(ICabinTypesService service)
        : base(service) { }
}
