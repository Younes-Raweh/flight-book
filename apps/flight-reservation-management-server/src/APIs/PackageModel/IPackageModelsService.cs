using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IPackageModelsService
{
    /// <summary>
    /// Create one Package
    /// </summary>
    public Task<PackageModel> CreatePackageModel(PackageModelCreateInput packagemodel);

    /// <summary>
    /// Delete one Package
    /// </summary>
    public Task DeletePackageModel(PackageModelWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Packages
    /// </summary>
    public Task<List<PackageModel>> PackageModels(PackageModelFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Package records
    /// </summary>
    public Task<MetadataDto> PackageModelsMeta(PackageModelFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Package
    /// </summary>
    public Task<PackageModel> PackageModel(PackageModelWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Package
    /// </summary>
    public Task UpdatePackageModel(
        PackageModelWhereUniqueInput uniqueId,
        PackageModelUpdateInput updateDto
    );
}
