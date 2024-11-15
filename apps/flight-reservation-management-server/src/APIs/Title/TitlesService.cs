using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class TitlesService : TitlesServiceBase
{
    public TitlesService(FlightReservationManagementDbContext context)
        : base(context) { }
}
