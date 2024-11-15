using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IPackageHotelsService
{
    /// <summary>
    /// Create one PackageHotel
    /// </summary>
    public Task<PackageHotel> CreatePackageHotel(PackageHotelCreateInput packagehotel);

    /// <summary>
    /// Delete one PackageHotel
    /// </summary>
    public Task DeletePackageHotel(PackageHotelWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many PackageHotels
    /// </summary>
    public Task<List<PackageHotel>> PackageHotels(PackageHotelFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about PackageHotel records
    /// </summary>
    public Task<MetadataDto> PackageHotelsMeta(PackageHotelFindManyArgs findManyArgs);

    /// <summary>
    /// Get one PackageHotel
    /// </summary>
    public Task<PackageHotel> PackageHotel(PackageHotelWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one PackageHotel
    /// </summary>
    public Task UpdatePackageHotel(
        PackageHotelWhereUniqueInput uniqueId,
        PackageHotelUpdateInput updateDto
    );
}
