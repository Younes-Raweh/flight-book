using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class HotelDealsService : HotelDealsServiceBase
{
    public HotelDealsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
