using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IVouchersService
{
    /// <summary>
    /// Create one Voucher
    /// </summary>
    public Task<Voucher> CreateVoucher(VoucherCreateInput voucher);

    /// <summary>
    /// Delete one Voucher
    /// </summary>
    public Task DeleteVoucher(VoucherWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Vouchers
    /// </summary>
    public Task<List<Voucher>> Vouchers(VoucherFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Voucher records
    /// </summary>
    public Task<MetadataDto> VouchersMeta(VoucherFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Voucher
    /// </summary>
    public Task<Voucher> Voucher(VoucherWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Voucher
    /// </summary>
    public Task UpdateVoucher(VoucherWhereUniqueInput uniqueId, VoucherUpdateInput updateDto);
}
