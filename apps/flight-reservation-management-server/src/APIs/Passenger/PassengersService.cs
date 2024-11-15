using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class PassengersService : PassengersServiceBase
{
    public PassengersService(FlightReservationManagementDbContext context)
        : base(context) { }
}
