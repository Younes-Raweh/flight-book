using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class CarBookingsService : CarBookingsServiceBase
{
    public CarBookingsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
