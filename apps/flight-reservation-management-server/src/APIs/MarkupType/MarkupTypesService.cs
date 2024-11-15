using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class MarkupTypesService : MarkupTypesServiceBase
{
    public MarkupTypesService(FlightReservationManagementDbContext context)
        : base(context) { }
}
