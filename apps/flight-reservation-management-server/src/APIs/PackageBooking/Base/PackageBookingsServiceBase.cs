using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class PackageBookingsServiceBase : IPackageBookingsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public PackageBookingsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one PackageBooking
    /// </summary>
    public async Task<PackageBooking> CreatePackageBooking(PackageBookingCreateInput createDto)
    {
        var packageBooking = new PackageBookingDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            packageBooking.Id = createDto.Id;
        }

        _context.PackageBookings.Add(packageBooking);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PackageBookingDbModel>(packageBooking.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one PackageBooking
    /// </summary>
    public async Task DeletePackageBooking(PackageBookingWhereUniqueInput uniqueId)
    {
        var packageBooking = await _context.PackageBookings.FindAsync(uniqueId.Id);
        if (packageBooking == null)
        {
            throw new NotFoundException();
        }

        _context.PackageBookings.Remove(packageBooking);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many PackageBookings
    /// </summary>
    public async Task<List<PackageBooking>> PackageBookings(PackageBookingFindManyArgs findManyArgs)
    {
        var packageBookings = await _context
            .PackageBookings.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return packageBookings.ConvertAll(packageBooking => packageBooking.ToDto());
    }

    /// <summary>
    /// Meta data about PackageBooking records
    /// </summary>
    public async Task<MetadataDto> PackageBookingsMeta(PackageBookingFindManyArgs findManyArgs)
    {
        var count = await _context.PackageBookings.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one PackageBooking
    /// </summary>
    public async Task<PackageBooking> PackageBooking(PackageBookingWhereUniqueInput uniqueId)
    {
        var packageBookings = await this.PackageBookings(
            new PackageBookingFindManyArgs
            {
                Where = new PackageBookingWhereInput { Id = uniqueId.Id }
            }
        );
        var packageBooking = packageBookings.FirstOrDefault();
        if (packageBooking == null)
        {
            throw new NotFoundException();
        }

        return packageBooking;
    }

    /// <summary>
    /// Update one PackageBooking
    /// </summary>
    public async Task UpdatePackageBooking(
        PackageBookingWhereUniqueInput uniqueId,
        PackageBookingUpdateInput updateDto
    )
    {
        var packageBooking = updateDto.ToModel(uniqueId);

        _context.Entry(packageBooking).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.PackageBookings.Any(e => e.Id == packageBooking.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
