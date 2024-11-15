using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class SightSeeingsService : SightSeeingsServiceBase
{
    public SightSeeingsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
