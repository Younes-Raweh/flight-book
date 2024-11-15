using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IPackageFlightsService
{
    /// <summary>
    /// Create one PackageFlight
    /// </summary>
    public Task<PackageFlight> CreatePackageFlight(PackageFlightCreateInput packageflight);

    /// <summary>
    /// Delete one PackageFlight
    /// </summary>
    public Task DeletePackageFlight(PackageFlightWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many PackageFlights
    /// </summary>
    public Task<List<PackageFlight>> PackageFlights(PackageFlightFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about PackageFlight records
    /// </summary>
    public Task<MetadataDto> PackageFlightsMeta(PackageFlightFindManyArgs findManyArgs);

    /// <summary>
    /// Get one PackageFlight
    /// </summary>
    public Task<PackageFlight> PackageFlight(PackageFlightWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one PackageFlight
    /// </summary>
    public Task UpdatePackageFlight(
        PackageFlightWhereUniqueInput uniqueId,
        PackageFlightUpdateInput updateDto
    );
}
