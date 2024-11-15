using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class PackageModelsService : PackageModelsServiceBase
{
    public PackageModelsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
