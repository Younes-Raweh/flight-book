using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class AttractionsService : AttractionsServiceBase
{
    public AttractionsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
