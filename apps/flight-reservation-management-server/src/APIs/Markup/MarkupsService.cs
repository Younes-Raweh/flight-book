using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class MarkupsService : MarkupsServiceBase
{
    public MarkupsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
