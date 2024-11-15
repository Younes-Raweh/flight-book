using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class BankPaymentsService : BankPaymentsServiceBase
{
    public BankPaymentsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
