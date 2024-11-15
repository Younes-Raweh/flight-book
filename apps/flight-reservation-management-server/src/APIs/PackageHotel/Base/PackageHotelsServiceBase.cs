using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class PackageHotelsServiceBase : IPackageHotelsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public PackageHotelsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one PackageHotel
    /// </summary>
    public async Task<PackageHotel> CreatePackageHotel(PackageHotelCreateInput createDto)
    {
        var packageHotel = new PackageHotelDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            packageHotel.Id = createDto.Id;
        }

        _context.PackageHotels.Add(packageHotel);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PackageHotelDbModel>(packageHotel.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one PackageHotel
    /// </summary>
    public async Task DeletePackageHotel(PackageHotelWhereUniqueInput uniqueId)
    {
        var packageHotel = await _context.PackageHotels.FindAsync(uniqueId.Id);
        if (packageHotel == null)
        {
            throw new NotFoundException();
        }

        _context.PackageHotels.Remove(packageHotel);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many PackageHotels
    /// </summary>
    public async Task<List<PackageHotel>> PackageHotels(PackageHotelFindManyArgs findManyArgs)
    {
        var packageHotels = await _context
            .PackageHotels.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return packageHotels.ConvertAll(packageHotel => packageHotel.ToDto());
    }

    /// <summary>
    /// Meta data about PackageHotel records
    /// </summary>
    public async Task<MetadataDto> PackageHotelsMeta(PackageHotelFindManyArgs findManyArgs)
    {
        var count = await _context.PackageHotels.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one PackageHotel
    /// </summary>
    public async Task<PackageHotel> PackageHotel(PackageHotelWhereUniqueInput uniqueId)
    {
        var packageHotels = await this.PackageHotels(
            new PackageHotelFindManyArgs { Where = new PackageHotelWhereInput { Id = uniqueId.Id } }
        );
        var packageHotel = packageHotels.FirstOrDefault();
        if (packageHotel == null)
        {
            throw new NotFoundException();
        }

        return packageHotel;
    }

    /// <summary>
    /// Update one PackageHotel
    /// </summary>
    public async Task UpdatePackageHotel(
        PackageHotelWhereUniqueInput uniqueId,
        PackageHotelUpdateInput updateDto
    )
    {
        var packageHotel = updateDto.ToModel(uniqueId);

        _context.Entry(packageHotel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.PackageHotels.Any(e => e.Id == packageHotel.Id))
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
