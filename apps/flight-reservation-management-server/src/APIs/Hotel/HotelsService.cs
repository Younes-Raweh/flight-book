using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class HotelsService : HotelsServiceBase
{
    public HotelsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
