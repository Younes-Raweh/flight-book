using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class WalletLogTypesService : WalletLogTypesServiceBase
{
    public WalletLogTypesService(FlightReservationManagementDbContext context)
        : base(context) { }
}
