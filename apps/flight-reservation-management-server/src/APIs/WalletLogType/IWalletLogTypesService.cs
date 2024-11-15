using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IWalletLogTypesService
{
    /// <summary>
    /// Create one WalletLogType
    /// </summary>
    public Task<WalletLogType> CreateWalletLogType(WalletLogTypeCreateInput walletlogtype);

    /// <summary>
    /// Delete one WalletLogType
    /// </summary>
    public Task DeleteWalletLogType(WalletLogTypeWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many WalletLogTypes
    /// </summary>
    public Task<List<WalletLogType>> WalletLogTypes(WalletLogTypeFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about WalletLogType records
    /// </summary>
    public Task<MetadataDto> WalletLogTypesMeta(WalletLogTypeFindManyArgs findManyArgs);

    /// <summary>
    /// Get one WalletLogType
    /// </summary>
    public Task<WalletLogType> WalletLogType(WalletLogTypeWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one WalletLogType
    /// </summary>
    public Task UpdateWalletLogType(
        WalletLogTypeWhereUniqueInput uniqueId,
        WalletLogTypeUpdateInput updateDto
    );
}
