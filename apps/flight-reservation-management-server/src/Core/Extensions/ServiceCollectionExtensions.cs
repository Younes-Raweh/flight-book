using FlightReservationManagement.APIs;

namespace FlightReservationManagement;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAgencyProfilesService, AgencyProfilesService>();
        services.AddScoped<IAirlinesService, AirlinesService>();
        services.AddScoped<IAirportsService, AirportsService>();
        services.AddScoped<IAttractionsService, AttractionsService>();
        services.AddScoped<IBanksService, BanksService>();
        services.AddScoped<IBankDetailsService, BankDetailsService>();
        services.AddScoped<IBankPaymentsService, BankPaymentsService>();
        services.AddScoped<ICabinTypesService, CabinTypesService>();
        services.AddScoped<ICarBookingsService, CarBookingsService>();
        services.AddScoped<ICommentsService, CommentsService>();
        services.AddScoped<ICooperateCustomerProfilesService, CooperateCustomerProfilesService>();
        services.AddScoped<IEmailSubscribersService, EmailSubscribersService>();
        services.AddScoped<IFlightsService, FlightsService>();
        services.AddScoped<IFlightBookingsService, FlightBookingsService>();
        services.AddScoped<IFlightDealsService, FlightDealsService>();
        services.AddScoped<IGalleriesService, GalleriesService>();
        services.AddScoped<IGendersService, GendersService>();
        services.AddScoped<IGoodToKnowsService, GoodToKnowsService>();
        services.AddScoped<IHotelsService, HotelsService>();
        services.AddScoped<IHotelBookingsService, HotelBookingsService>();
        services.AddScoped<IHotelDealsService, HotelDealsService>();
        services.AddScoped<IInvoicesService, InvoicesService>();
        services.AddScoped<IMarkdownsService, MarkdownsService>();
        services.AddScoped<IMarkupsService, MarkupsService>();
        services.AddScoped<IMarkupTypesService, MarkupTypesService>();
        services.AddScoped<IMarkupValueTypesService, MarkupValueTypesService>();
        services.AddScoped<INotificationsService, NotificationsService>();
        services.AddScoped<IOnlinePaymentsService, OnlinePaymentsService>();
        services.AddScoped<IPackageAttractionsService, PackageAttractionsService>();
        services.AddScoped<IPackageBookingsService, PackageBookingsService>();
        services.AddScoped<IPackageCategoriesService, PackageCategoriesService>();
        services.AddScoped<IPackageFlightsService, PackageFlightsService>();
        services.AddScoped<IPackageHotelsService, PackageHotelsService>();
        services.AddScoped<IPackageModelsService, PackageModelsService>();
        services.AddScoped<IPackageTypesService, PackageTypesService>();
        services.AddScoped<IPassengersService, PassengersService>();
        services.AddScoped<IPayLatersService, PayLatersService>();
        services.AddScoped<IPaymentsService, PaymentsService>();
        services.AddScoped<IProfilesService, ProfilesService>();
        services.AddScoped<IReservationsService, ReservationsService>();
        services.AddScoped<IRolesService, RolesService>();
        services.AddScoped<IRoleUsersService, RoleUsersService>();
        services.AddScoped<ISightSeeingsService, SightSeeingsService>();
        services.AddScoped<ITitlesService, TitlesService>();
        services.AddScoped<ITourOperatorsService, TourOperatorsService>();
        services.AddScoped<ITravelPackagesService, TravelPackagesService>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IVatsService, VatsService>();
        services.AddScoped<IVisaApplicationsService, VisaApplicationsService>();
        services.AddScoped<IVouchersService, VouchersService>();
        services.AddScoped<IWalletsService, WalletsService>();
        services.AddScoped<IWalletLogsService, WalletLogsService>();
        services.AddScoped<IWalletLogTypesService, WalletLogTypesService>();
    }
}
