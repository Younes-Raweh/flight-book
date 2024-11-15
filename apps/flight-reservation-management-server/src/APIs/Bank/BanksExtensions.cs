using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class BanksExtensions
{
    public static Bank ToDto(this BankDbModel model)
    {
        return new Bank
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static BankDbModel ToModel(this BankUpdateInput updateDto, BankWhereUniqueInput uniqueId)
    {
        var bank = new BankDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            bank.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            bank.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return bank;
    }
}
