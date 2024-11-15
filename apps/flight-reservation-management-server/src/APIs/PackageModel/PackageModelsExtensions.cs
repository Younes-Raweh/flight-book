using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class PackageModelsExtensions
{
    public static PackageModel ToDto(this PackageModelDbModel model)
    {
        return new PackageModel
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PackageModelDbModel ToModel(
        this PackageModelUpdateInput updateDto,
        PackageModelWhereUniqueInput uniqueId
    )
    {
        var packageModel = new PackageModelDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            packageModel.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            packageModel.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return packageModel;
    }
}
