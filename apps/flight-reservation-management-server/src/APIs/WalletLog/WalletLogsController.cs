using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[ApiController()]
public class WalletLogsController : WalletLogsControllerBase
{
    public WalletLogsController(IWalletLogsService service)
        : base(service) { }
}
