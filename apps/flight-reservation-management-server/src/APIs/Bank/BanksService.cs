using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class BanksService : BanksServiceBase
{
    public BanksService(FlightReservationManagementDbContext context)
        : base(context) { }
}
