using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class WalletsExtensions
{
    public static Wallet ToDto(this WalletDbModel model)
    {
        return new Wallet
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static WalletDbModel ToModel(
        this WalletUpdateInput updateDto,
        WalletWhereUniqueInput uniqueId
    )
    {
        var wallet = new WalletDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            wallet.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            wallet.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return wallet;
    }
}
