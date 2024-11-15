using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class InvoicesService : InvoicesServiceBase
{
    public InvoicesService(FlightReservationManagementDbContext context)
        : base(context) { }
}
