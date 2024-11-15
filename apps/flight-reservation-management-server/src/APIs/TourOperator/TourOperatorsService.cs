using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class TourOperatorsService : TourOperatorsServiceBase
{
    public TourOperatorsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
