using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class TravelPackagesService : TravelPackagesServiceBase
{
    public TravelPackagesService(FlightReservationManagementDbContext context)
        : base(context) { }
}
