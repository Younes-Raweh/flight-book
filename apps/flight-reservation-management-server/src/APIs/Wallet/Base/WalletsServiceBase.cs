using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class WalletsServiceBase : IWalletsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public WalletsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Wallet
    /// </summary>
    public async Task<Wallet> CreateWallet(WalletCreateInput createDto)
    {
        var wallet = new WalletDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            wallet.Id = createDto.Id;
        }

        _context.Wallets.Add(wallet);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<WalletDbModel>(wallet.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Wallet
    /// </summary>
    public async Task DeleteWallet(WalletWhereUniqueInput uniqueId)
    {
        var wallet = await _context.Wallets.FindAsync(uniqueId.Id);
        if (wallet == null)
        {
            throw new NotFoundException();
        }

        _context.Wallets.Remove(wallet);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Wallets
    /// </summary>
    public async Task<List<Wallet>> Wallets(WalletFindManyArgs findManyArgs)
    {
        var wallets = await _context
            .Wallets.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return wallets.ConvertAll(wallet => wallet.ToDto());
    }

    /// <summary>
    /// Meta data about Wallet records
    /// </summary>
    public async Task<MetadataDto> WalletsMeta(WalletFindManyArgs findManyArgs)
    {
        var count = await _context.Wallets.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Wallet
    /// </summary>
    public async Task<Wallet> Wallet(WalletWhereUniqueInput uniqueId)
    {
        var wallets = await this.Wallets(
            new WalletFindManyArgs { Where = new WalletWhereInput { Id = uniqueId.Id } }
        );
        var wallet = wallets.FirstOrDefault();
        if (wallet == null)
        {
            throw new NotFoundException();
        }

        return wallet;
    }

    /// <summary>
    /// Update one Wallet
    /// </summary>
    public async Task UpdateWallet(WalletWhereUniqueInput uniqueId, WalletUpdateInput updateDto)
    {
        var wallet = updateDto.ToModel(uniqueId);

        _context.Entry(wallet).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Wallets.Any(e => e.Id == wallet.Id))
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
