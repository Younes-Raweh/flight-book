using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class RoleUsersService : RoleUsersServiceBase
{
    public RoleUsersService(FlightReservationManagementDbContext context)
        : base(context) { }
}
