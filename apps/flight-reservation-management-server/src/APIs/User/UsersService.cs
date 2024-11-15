using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(FlightReservationManagementDbContext context)
        : base(context) { }
}
