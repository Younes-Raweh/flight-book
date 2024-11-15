using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class WalletLogTypesController : WalletLogTypesControllerBase
{
    public WalletLogTypesController(IWalletLogTypesService service)
        : base(service) { }
}
