using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class BankDetailsService : BankDetailsServiceBase
{
    public BankDetailsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
