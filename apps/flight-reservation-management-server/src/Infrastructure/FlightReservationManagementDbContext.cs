using FlightReservationManagement.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.Infrastructure;

public class FlightReservationManagementDbContext : IdentityDbContext<IdentityUser>
{
    public FlightReservationManagementDbContext(
        DbContextOptions<FlightReservationManagementDbContext> options
    )
        : base(options) { }

    public DbSet<PassengerDbModel> Passengers { get; set; }

    public DbSet<ReservationDbModel> Reservations { get; set; }

    public DbSet<FlightDbModel> Flights { get; set; }

    public DbSet<TourOperatorDbModel> TourOperators { get; set; }

    public DbSet<NotificationDbModel> Notifications { get; set; }

    public DbSet<InvoiceDbModel> Invoices { get; set; }

    public DbSet<PaymentDbModel> Payments { get; set; }

    public DbSet<CarBookingDbModel> CarBookings { get; set; }

    public DbSet<CabinTypeDbModel> CabinTypes { get; set; }

    public DbSet<GalleryDbModel> Galleries { get; set; }

    public DbSet<CommentDbModel> Comments { get; set; }

    public DbSet<AirportDbModel> Airports { get; set; }

    public DbSet<AirlineDbModel> Airlines { get; set; }

    public DbSet<BankPaymentDbModel> BankPayments { get; set; }

    public DbSet<PayLaterDbModel> PayLaters { get; set; }

    public DbSet<AttractionDbModel> Attractions { get; set; }

    public DbSet<EmailSubscriberDbModel> EmailSubscribers { get; set; }

    public DbSet<WalletLogTypeDbModel> WalletLogTypes { get; set; }

    public DbSet<BankDbModel> Banks { get; set; }

    public DbSet<AgencyProfileDbModel> AgencyProfiles { get; set; }

    public DbSet<RoleUserDbModel> RoleUsers { get; set; }

    public DbSet<SightSeeingDbModel> SightSeeings { get; set; }

    public DbSet<FlightDealDbModel> FlightDeals { get; set; }

    public DbSet<RoleDbModel> Roles { get; set; }

    public DbSet<CooperateCustomerProfileDbModel> CooperateCustomerProfiles { get; set; }

    public DbSet<ProfileDbModel> Profiles { get; set; }

    public DbSet<FlightBookingDbModel> FlightBookings { get; set; }

    public DbSet<BankDetailDbModel> BankDetails { get; set; }

    public DbSet<TravelPackageDbModel> TravelPackages { get; set; }

    public DbSet<PackageBookingDbModel> PackageBookings { get; set; }

    public DbSet<PackageAttractionDbModel> PackageAttractions { get; set; }

    public DbSet<VoucherDbModel> Vouchers { get; set; }

    public DbSet<VatDbModel> Vats { get; set; }

    public DbSet<MarkupTypeDbModel> MarkupTypes { get; set; }

    public DbSet<VisaApplicationDbModel> VisaApplications { get; set; }

    public DbSet<MarkupDbModel> Markups { get; set; }

    public DbSet<TitleDbModel> Titles { get; set; }

    public DbSet<WalletDbModel> Wallets { get; set; }

    public DbSet<OnlinePaymentDbModel> OnlinePayments { get; set; }

    public DbSet<MarkdownDbModel> Markdowns { get; set; }

    public DbSet<MarkupValueTypeDbModel> MarkupValueTypes { get; set; }

    public DbSet<HotelDealDbModel> HotelDeals { get; set; }

    public DbSet<PackageCategoryDbModel> PackageCategories { get; set; }

    public DbSet<HotelDbModel> Hotels { get; set; }

    public DbSet<GenderDbModel> Genders { get; set; }

    public DbSet<HotelBookingDbModel> HotelBookings { get; set; }

    public DbSet<PackageModelDbModel> PackageModels { get; set; }

    public DbSet<UserDbModel> Users { get; set; }

    public DbSet<GoodToKnowDbModel> GoodToKnows { get; set; }

    public DbSet<WalletLogDbModel> WalletLogs { get; set; }

    public DbSet<PackageFlightDbModel> PackageFlights { get; set; }

    public DbSet<PackageHotelDbModel> PackageHotels { get; set; }

    public DbSet<PackageTypeDbModel> PackageTypes { get; set; }
}
