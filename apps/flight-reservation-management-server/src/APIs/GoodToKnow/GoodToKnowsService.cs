using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class GoodToKnowsService : GoodToKnowsServiceBase
{
    public GoodToKnowsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
