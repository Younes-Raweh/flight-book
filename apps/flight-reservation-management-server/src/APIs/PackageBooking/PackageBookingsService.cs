using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class PackageBookingsService : PackageBookingsServiceBase
{
    public PackageBookingsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
