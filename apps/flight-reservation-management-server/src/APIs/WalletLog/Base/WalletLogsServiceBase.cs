using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class WalletLogsServiceBase : IWalletLogsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public WalletLogsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one WalletLog
    /// </summary>
    public async Task<WalletLog> CreateWalletLog(WalletLogCreateInput createDto)
    {
        var walletLog = new WalletLogDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            walletLog.Id = createDto.Id;
        }

        _context.WalletLogs.Add(walletLog);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<WalletLogDbModel>(walletLog.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one WalletLog
    /// </summary>
    public async Task DeleteWalletLog(WalletLogWhereUniqueInput uniqueId)
    {
        var walletLog = await _context.WalletLogs.FindAsync(uniqueId.Id);
        if (walletLog == null)
        {
            throw new NotFoundException();
        }

        _context.WalletLogs.Remove(walletLog);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many WalletLogs
    /// </summary>
    public async Task<List<WalletLog>> WalletLogs(WalletLogFindManyArgs findManyArgs)
    {
        var walletLogs = await _context
            .WalletLogs.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return walletLogs.ConvertAll(walletLog => walletLog.ToDto());
    }

    /// <summary>
    /// Meta data about WalletLog records
    /// </summary>
    public async Task<MetadataDto> WalletLogsMeta(WalletLogFindManyArgs findManyArgs)
    {
        var count = await _context.WalletLogs.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one WalletLog
    /// </summary>
    public async Task<WalletLog> WalletLog(WalletLogWhereUniqueInput uniqueId)
    {
        var walletLogs = await this.WalletLogs(
            new WalletLogFindManyArgs { Where = new WalletLogWhereInput { Id = uniqueId.Id } }
        );
        var walletLog = walletLogs.FirstOrDefault();
        if (walletLog == null)
        {
            throw new NotFoundException();
        }

        return walletLog;
    }

    /// <summary>
    /// Update one WalletLog
    /// </summary>
    public async Task UpdateWalletLog(
        WalletLogWhereUniqueInput uniqueId,
        WalletLogUpdateInput updateDto
    )
    {
        var walletLog = updateDto.ToModel(uniqueId);

        _context.Entry(walletLog).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.WalletLogs.Any(e => e.Id == walletLog.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
