using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class WalletLogTypesServiceBase : IWalletLogTypesService
{
    protected readonly FlightReservationManagementDbContext _context;

    public WalletLogTypesServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one WalletLogType
    /// </summary>
    public async Task<WalletLogType> CreateWalletLogType(WalletLogTypeCreateInput createDto)
    {
        var walletLogType = new WalletLogTypeDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            walletLogType.Id = createDto.Id;
        }

        _context.WalletLogTypes.Add(walletLogType);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<WalletLogTypeDbModel>(walletLogType.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one WalletLogType
    /// </summary>
    public async Task DeleteWalletLogType(WalletLogTypeWhereUniqueInput uniqueId)
    {
        var walletLogType = await _context.WalletLogTypes.FindAsync(uniqueId.Id);
        if (walletLogType == null)
        {
            throw new NotFoundException();
        }

        _context.WalletLogTypes.Remove(walletLogType);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many WalletLogTypes
    /// </summary>
    public async Task<List<WalletLogType>> WalletLogTypes(WalletLogTypeFindManyArgs findManyArgs)
    {
        var walletLogTypes = await _context
            .WalletLogTypes.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return walletLogTypes.ConvertAll(walletLogType => walletLogType.ToDto());
    }

    /// <summary>
    /// Meta data about WalletLogType records
    /// </summary>
    public async Task<MetadataDto> WalletLogTypesMeta(WalletLogTypeFindManyArgs findManyArgs)
    {
        var count = await _context.WalletLogTypes.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one WalletLogType
    /// </summary>
    public async Task<WalletLogType> WalletLogType(WalletLogTypeWhereUniqueInput uniqueId)
    {
        var walletLogTypes = await this.WalletLogTypes(
            new WalletLogTypeFindManyArgs
            {
                Where = new WalletLogTypeWhereInput { Id = uniqueId.Id }
            }
        );
        var walletLogType = walletLogTypes.FirstOrDefault();
        if (walletLogType == null)
        {
            throw new NotFoundException();
        }

        return walletLogType;
    }

    /// <summary>
    /// Update one WalletLogType
    /// </summary>
    public async Task UpdateWalletLogType(
        WalletLogTypeWhereUniqueInput uniqueId,
        WalletLogTypeUpdateInput updateDto
    )
    {
        var walletLogType = updateDto.ToModel(uniqueId);

        _context.Entry(walletLogType).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.WalletLogTypes.Any(e => e.Id == walletLogType.Id))
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
