using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class ProfilesService : ProfilesServiceBase
{
    public ProfilesService(FlightReservationManagementDbContext context)
        : base(context) { }
}
