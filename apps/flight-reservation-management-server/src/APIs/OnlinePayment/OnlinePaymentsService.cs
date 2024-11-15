using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class OnlinePaymentsService : OnlinePaymentsServiceBase
{
    public OnlinePaymentsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
