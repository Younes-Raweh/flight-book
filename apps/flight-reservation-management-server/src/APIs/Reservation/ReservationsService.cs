using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class ReservationsService : ReservationsServiceBase
{
    public ReservationsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
