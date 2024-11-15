using FlightReservationManagement.APIs;

namespace FlightReservationManagement;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IFlightsService, FlightsService>();
        services.AddScoped<IInvoicesService, InvoicesService>();
        services.AddScoped<INotificationsService, NotificationsService>();
        services.AddScoped<IPassengersService, PassengersService>();
        services.AddScoped<IPaymentsService, PaymentsService>();
        services.AddScoped<IReservationsService, ReservationsService>();
        services.AddScoped<ITourOperatorsService, TourOperatorsService>();
    }
}
