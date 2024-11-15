using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IWalletLogsService
{
    /// <summary>
    /// Create one WalletLog
    /// </summary>
    public Task<WalletLog> CreateWalletLog(WalletLogCreateInput walletlog);

    /// <summary>
    /// Delete one WalletLog
    /// </summary>
    public Task DeleteWalletLog(WalletLogWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many WalletLogs
    /// </summary>
    public Task<List<WalletLog>> WalletLogs(WalletLogFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about WalletLog records
    /// </summary>
    public Task<MetadataDto> WalletLogsMeta(WalletLogFindManyArgs findManyArgs);

    /// <summary>
    /// Get one WalletLog
    /// </summary>
    public Task<WalletLog> WalletLog(WalletLogWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one WalletLog
    /// </summary>
    public Task UpdateWalletLog(WalletLogWhereUniqueInput uniqueId, WalletLogUpdateInput updateDto);
}
