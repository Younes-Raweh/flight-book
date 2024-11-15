using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IWalletsService
{
    /// <summary>
    /// Create one Wallet
    /// </summary>
    public Task<Wallet> CreateWallet(WalletCreateInput wallet);

    /// <summary>
    /// Delete one Wallet
    /// </summary>
    public Task DeleteWallet(WalletWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Wallets
    /// </summary>
    public Task<List<Wallet>> Wallets(WalletFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Wallet records
    /// </summary>
    public Task<MetadataDto> WalletsMeta(WalletFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Wallet
    /// </summary>
    public Task<Wallet> Wallet(WalletWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Wallet
    /// </summary>
    public Task UpdateWallet(WalletWhereUniqueInput uniqueId, WalletUpdateInput updateDto);
}
