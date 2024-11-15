using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class VouchersServiceBase : IVouchersService
{
    protected readonly FlightReservationManagementDbContext _context;

    public VouchersServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Voucher
    /// </summary>
    public async Task<Voucher> CreateVoucher(VoucherCreateInput createDto)
    {
        var voucher = new VoucherDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            voucher.Id = createDto.Id;
        }

        _context.Vouchers.Add(voucher);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<VoucherDbModel>(voucher.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Voucher
    /// </summary>
    public async Task DeleteVoucher(VoucherWhereUniqueInput uniqueId)
    {
        var voucher = await _context.Vouchers.FindAsync(uniqueId.Id);
        if (voucher == null)
        {
            throw new NotFoundException();
        }

        _context.Vouchers.Remove(voucher);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Vouchers
    /// </summary>
    public async Task<List<Voucher>> Vouchers(VoucherFindManyArgs findManyArgs)
    {
        var vouchers = await _context
            .Vouchers.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return vouchers.ConvertAll(voucher => voucher.ToDto());
    }

    /// <summary>
    /// Meta data about Voucher records
    /// </summary>
    public async Task<MetadataDto> VouchersMeta(VoucherFindManyArgs findManyArgs)
    {
        var count = await _context.Vouchers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Voucher
    /// </summary>
    public async Task<Voucher> Voucher(VoucherWhereUniqueInput uniqueId)
    {
        var vouchers = await this.Vouchers(
            new VoucherFindManyArgs { Where = new VoucherWhereInput { Id = uniqueId.Id } }
        );
        var voucher = vouchers.FirstOrDefault();
        if (voucher == null)
        {
            throw new NotFoundException();
        }

        return voucher;
    }

    /// <summary>
    /// Update one Voucher
    /// </summary>
    public async Task UpdateVoucher(VoucherWhereUniqueInput uniqueId, VoucherUpdateInput updateDto)
    {
        var voucher = updateDto.ToModel(uniqueId);

        _context.Entry(voucher).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Vouchers.Any(e => e.Id == voucher.Id))
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
