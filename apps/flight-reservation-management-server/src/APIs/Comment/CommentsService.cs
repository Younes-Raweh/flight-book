using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class CommentsService : CommentsServiceBase
{
    public CommentsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
