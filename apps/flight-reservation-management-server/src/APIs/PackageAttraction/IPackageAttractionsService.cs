using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IPackageAttractionsService
{
    /// <summary>
    /// Create one PackageAttraction
    /// </summary>
    public Task<PackageAttraction> CreatePackageAttraction(
        PackageAttractionCreateInput packageattraction
    );

    /// <summary>
    /// Delete one PackageAttraction
    /// </summary>
    public Task DeletePackageAttraction(PackageAttractionWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many PackageAttractions
    /// </summary>
    public Task<List<PackageAttraction>> PackageAttractions(
        PackageAttractionFindManyArgs findManyArgs
    );

    /// <summary>
    /// Meta data about PackageAttraction records
    /// </summary>
    public Task<MetadataDto> PackageAttractionsMeta(PackageAttractionFindManyArgs findManyArgs);

    /// <summary>
    /// Get one PackageAttraction
    /// </summary>
    public Task<PackageAttraction> PackageAttraction(PackageAttractionWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one PackageAttraction
    /// </summary>
    public Task UpdatePackageAttraction(
        PackageAttractionWhereUniqueInput uniqueId,
        PackageAttractionUpdateInput updateDto
    );
}
