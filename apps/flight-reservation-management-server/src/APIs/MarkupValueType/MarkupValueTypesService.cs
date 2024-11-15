using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class MarkupValueTypesService : MarkupValueTypesServiceBase
{
    public MarkupValueTypesService(FlightReservationManagementDbContext context)
        : base(context) { }
}
