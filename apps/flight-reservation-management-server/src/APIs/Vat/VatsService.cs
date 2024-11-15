using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class VatsService : VatsServiceBase
{
    public VatsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
