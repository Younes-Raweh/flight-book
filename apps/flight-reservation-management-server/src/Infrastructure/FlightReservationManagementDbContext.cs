using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.Infrastructure;

public class FlightReservationManagementDbContext : DbContext
{
    public FlightReservationManagementDbContext(
        DbContextOptions<FlightReservationManagementDbContext> options
    )
        : base(options) { }

    public DbSet<PassengerDbModel> Passengers { get; set; }

    public DbSet<ReservationDbModel> Reservations { get; set; }

    public DbSet<NotificationDbModel> Notifications { get; set; }

    public DbSet<InvoiceDbModel> Invoices { get; set; }

    public DbSet<FlightDbModel> Flights { get; set; }

    public DbSet<PaymentDbModel> Payments { get; set; }

    public DbSet<TourOperatorDbModel> TourOperators { get; set; }
}
