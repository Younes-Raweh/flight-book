using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class GendersService : GendersServiceBase
{
    public GendersService(FlightReservationManagementDbContext context)
        : base(context) { }
}
