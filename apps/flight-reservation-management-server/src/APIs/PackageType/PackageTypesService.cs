using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class PackageTypesService : PackageTypesServiceBase
{
    public PackageTypesService(FlightReservationManagementDbContext context)
        : base(context) { }
}
