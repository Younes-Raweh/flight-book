using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class OnlinePaymentsExtensions
{
    public static OnlinePayment ToDto(this OnlinePaymentDbModel model)
    {
        return new OnlinePayment
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static OnlinePaymentDbModel ToModel(
        this OnlinePaymentUpdateInput updateDto,
        OnlinePaymentWhereUniqueInput uniqueId
    )
    {
        var onlinePayment = new OnlinePaymentDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            onlinePayment.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            onlinePayment.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return onlinePayment;
    }
}
