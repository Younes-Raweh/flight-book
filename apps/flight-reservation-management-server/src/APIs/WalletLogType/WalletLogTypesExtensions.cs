using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class WalletLogTypesExtensions
{
    public static WalletLogType ToDto(this WalletLogTypeDbModel model)
    {
        return new WalletLogType
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static WalletLogTypeDbModel ToModel(
        this WalletLogTypeUpdateInput updateDto,
        WalletLogTypeWhereUniqueInput uniqueId
    )
    {
        var walletLogType = new WalletLogTypeDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            walletLogType.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            walletLogType.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return walletLogType;
    }
}
