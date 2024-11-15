using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class MarkdownsService : MarkdownsServiceBase
{
    public MarkdownsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
