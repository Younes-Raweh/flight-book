using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class BankPaymentsExtensions
{
    public static BankPayment ToDto(this BankPaymentDbModel model)
    {
        return new BankPayment
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static BankPaymentDbModel ToModel(
        this BankPaymentUpdateInput updateDto,
        BankPaymentWhereUniqueInput uniqueId
    )
    {
        var bankPayment = new BankPaymentDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            bankPayment.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            bankPayment.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return bankPayment;
    }
}
