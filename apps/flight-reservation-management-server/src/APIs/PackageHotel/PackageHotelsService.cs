using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class PackageHotelsService : PackageHotelsServiceBase
{
    public PackageHotelsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
