using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class PackageAttractionsService : PackageAttractionsServiceBase
{
    public PackageAttractionsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
