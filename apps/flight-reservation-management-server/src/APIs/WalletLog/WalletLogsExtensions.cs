using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class WalletLogsExtensions
{
    public static WalletLog ToDto(this WalletLogDbModel model)
    {
        return new WalletLog
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static WalletLogDbModel ToModel(
        this WalletLogUpdateInput updateDto,
        WalletLogWhereUniqueInput uniqueId
    )
    {
        var walletLog = new WalletLogDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            walletLog.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            walletLog.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return walletLog;
    }
}
