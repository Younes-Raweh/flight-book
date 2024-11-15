using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IPackageBookingsService
{
    /// <summary>
    /// Create one PackageBooking
    /// </summary>
    public Task<PackageBooking> CreatePackageBooking(PackageBookingCreateInput packagebooking);

    /// <summary>
    /// Delete one PackageBooking
    /// </summary>
    public Task DeletePackageBooking(PackageBookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many PackageBookings
    /// </summary>
    public Task<List<PackageBooking>> PackageBookings(PackageBookingFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about PackageBooking records
    /// </summary>
    public Task<MetadataDto> PackageBookingsMeta(PackageBookingFindManyArgs findManyArgs);

    /// <summary>
    /// Get one PackageBooking
    /// </summary>
    public Task<PackageBooking> PackageBooking(PackageBookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one PackageBooking
    /// </summary>
    public Task UpdatePackageBooking(
        PackageBookingWhereUniqueInput uniqueId,
        PackageBookingUpdateInput updateDto
    );
}
