using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class WalletsService : WalletsServiceBase
{
    public WalletsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
