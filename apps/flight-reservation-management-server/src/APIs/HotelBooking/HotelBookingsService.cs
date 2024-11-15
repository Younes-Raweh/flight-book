using FlightReservationManagement.Infrastructure;

namespace FlightReservationManagement.APIs;

public class HotelBookingsService : HotelBookingsServiceBase
{
    public HotelBookingsService(FlightReservationManagementDbContext context)
        : base(context) { }
}
