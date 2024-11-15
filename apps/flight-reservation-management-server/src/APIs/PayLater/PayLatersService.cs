using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class PayLatersService : PayLatersServiceBase
{
    public PayLatersService(FlightReservationManagementDbContext context)
        : base(context) { }
}
