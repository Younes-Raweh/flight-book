using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class WalletLogsService : WalletLogsServiceBase
{
    public WalletLogsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
