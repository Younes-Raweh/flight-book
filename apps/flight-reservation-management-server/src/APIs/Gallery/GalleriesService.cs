using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class GalleriesService : GalleriesServiceBase
{
    public GalleriesService(FlightReservationManagementDbContext context)
        : base(context) { }
}
