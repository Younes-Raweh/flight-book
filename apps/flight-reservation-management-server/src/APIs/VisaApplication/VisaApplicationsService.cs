using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class VisaApplicationsService : VisaApplicationsServiceBase
{
    public VisaApplicationsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
