using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class CooperateCustomerProfilesController : CooperateCustomerProfilesControllerBase
{
    public CooperateCustomerProfilesController(ICooperateCustomerProfilesService service)
        : base(service) { }
}
