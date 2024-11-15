using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class CooperateCustomerProfilesService : CooperateCustomerProfilesServiceBase
{
    public CooperateCustomerProfilesService(FlightReservationManagementDbContext context)
        : base(context) { }
}
