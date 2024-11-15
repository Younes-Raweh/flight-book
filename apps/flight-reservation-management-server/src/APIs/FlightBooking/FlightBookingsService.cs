using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class FlightBookingsService : FlightBookingsServiceBase
{
    public FlightBookingsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
