using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class FlightDealsService : FlightDealsServiceBase
{
    public FlightDealsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
