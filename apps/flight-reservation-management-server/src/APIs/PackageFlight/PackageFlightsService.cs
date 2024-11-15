using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class PackageFlightsService : PackageFlightsServiceBase
{
    public PackageFlightsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
