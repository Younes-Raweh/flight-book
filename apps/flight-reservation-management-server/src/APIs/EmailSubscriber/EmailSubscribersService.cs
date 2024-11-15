using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class EmailSubscribersService : EmailSubscribersServiceBase
{
    public EmailSubscribersService(FlightReservationManagementDbContext context)
        : base(context) { }
}
