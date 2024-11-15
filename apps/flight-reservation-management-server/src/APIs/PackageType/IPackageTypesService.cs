using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IPackageTypesService
{
    /// <summary>
    /// Create one PackageType
    /// </summary>
    public Task<PackageType> CreatePackageType(PackageTypeCreateInput packagetype);

    /// <summary>
    /// Delete one PackageType
    /// </summary>
    public Task DeletePackageType(PackageTypeWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many PackageTypes
    /// </summary>
    public Task<List<PackageType>> PackageTypes(PackageTypeFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about PackageType records
    /// </summary>
    public Task<MetadataDto> PackageTypesMeta(PackageTypeFindManyArgs findManyArgs);

    /// <summary>
    /// Get one PackageType
    /// </summary>
    public Task<PackageType> PackageType(PackageTypeWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one PackageType
    /// </summary>
    public Task UpdatePackageType(
        PackageTypeWhereUniqueInput uniqueId,
        PackageTypeUpdateInput updateDto
    );
}
