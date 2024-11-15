using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class BankDetailsExtensions
{
    public static BankDetail ToDto(this BankDetailDbModel model)
    {
        return new BankDetail
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static BankDetailDbModel ToModel(
        this BankDetailUpdateInput updateDto,
        BankDetailWhereUniqueInput uniqueId
    )
    {
        var bankDetail = new BankDetailDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            bankDetail.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            bankDetail.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return bankDetail;
    }
}
