using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class VatsExtensions
{
    public static Vat ToDto(this VatDbModel model)
    {
        return new Vat
        {
            CarVatType = model.CarVatType,
            CarVatValue = model.CarVatValue,
            CreatedAt = model.CreatedAt,
            FlightVatType = model.FlightVatType,
            FlightVatValue = model.FlightVatValue,
            HotelVatType = model.HotelVatType,
            HotelVatValue = model.HotelVatValue,
            Id = model.Id,
            PackageVatType = model.PackageVatType,
            PackageVatValue = model.PackageVatValue,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static VatDbModel ToModel(this VatUpdateInput updateDto, VatWhereUniqueInput uniqueId)
    {
        var vat = new VatDbModel
        {
            Id = uniqueId.Id,
            CarVatType = updateDto.CarVatType,
            CarVatValue = updateDto.CarVatValue,
            FlightVatType = updateDto.FlightVatType,
            FlightVatValue = updateDto.FlightVatValue,
            HotelVatType = updateDto.HotelVatType,
            HotelVatValue = updateDto.HotelVatValue,
            PackageVatType = updateDto.PackageVatType,
            PackageVatValue = updateDto.PackageVatValue
        };

        if (updateDto.CreatedAt != null)
        {
            vat.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            vat.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return vat;
    }
}
