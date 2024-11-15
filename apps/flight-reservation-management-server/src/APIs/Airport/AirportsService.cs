using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class AirportsService : AirportsServiceBase
{
    public AirportsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
