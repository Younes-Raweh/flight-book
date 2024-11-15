using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class AgencyProfilesService : AgencyProfilesServiceBase
{
    public AgencyProfilesService(FlightReservationManagementDbContext context)
        : base(context) { }
}
