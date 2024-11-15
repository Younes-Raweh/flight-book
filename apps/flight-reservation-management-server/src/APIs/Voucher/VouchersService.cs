using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class VouchersService : VouchersServiceBase
{
    public VouchersService(FlightReservationManagementDbContext context)
        : base(context) { }
}
