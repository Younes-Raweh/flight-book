using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class VouchersExtensions
{
    public static Voucher ToDto(this VoucherDbModel model)
    {
        return new Voucher
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static VoucherDbModel ToModel(
        this VoucherUpdateInput updateDto,
        VoucherWhereUniqueInput uniqueId
    )
    {
        var voucher = new VoucherDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            voucher.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            voucher.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return voucher;
    }
}
