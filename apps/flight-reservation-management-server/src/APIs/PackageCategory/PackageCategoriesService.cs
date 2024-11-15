using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class PackageCategoriesService : PackageCategoriesServiceBase
{
    public PackageCategoriesService(FlightReservationManagementDbContext context)
        : base(context) { }
}
