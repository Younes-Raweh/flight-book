using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class FlightsService : FlightsServiceBase
{
    public FlightsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
