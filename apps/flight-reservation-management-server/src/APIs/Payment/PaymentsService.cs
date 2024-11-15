using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class PaymentsService : PaymentsServiceBase
{
    public PaymentsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
