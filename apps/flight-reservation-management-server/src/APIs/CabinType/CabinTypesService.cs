using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class CabinTypesService : CabinTypesServiceBase
{
    public CabinTypesService(FlightReservationManagementDbContext context)
        : base(context) { }
}
