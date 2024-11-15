using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface ITravelPackagesService
{
    /// <summary>
    /// Create one TravelPackage
    /// </summary>
    public Task<TravelPackage> CreateTravelPackage(TravelPackageCreateInput travelpackage);

    /// <summary>
    /// Delete one TravelPackage
    /// </summary>
    public Task DeleteTravelPackage(TravelPackageWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many TravelPackages
    /// </summary>
    public Task<List<TravelPackage>> TravelPackages(TravelPackageFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about TravelPackage records
    /// </summary>
    public Task<MetadataDto> TravelPackagesMeta(TravelPackageFindManyArgs findManyArgs);

    /// <summary>
    /// Get one TravelPackage
    /// </summary>
    public Task<TravelPackage> TravelPackage(TravelPackageWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one TravelPackage
    /// </summary>
    public Task UpdateTravelPackage(
        TravelPackageWhereUniqueInput uniqueId,
        TravelPackageUpdateInput updateDto
    );
}
