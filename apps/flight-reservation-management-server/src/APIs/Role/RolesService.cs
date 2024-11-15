using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class RolesService : RolesServiceBase
{
    public RolesService(FlightReservationManagementDbContext context)
        : base(context) { }
}
