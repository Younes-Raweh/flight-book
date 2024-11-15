using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class AirlinesService : AirlinesServiceBase
{
    public AirlinesService(FlightReservationManagementDbContext context)
        : base(context) { }
}
