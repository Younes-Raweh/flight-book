using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class PackageFlightsServiceBase : IPackageFlightsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public PackageFlightsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one PackageFlight
    /// </summary>
    public async Task<PackageFlight> CreatePackageFlight(PackageFlightCreateInput createDto)
    {
        var packageFlight = new PackageFlightDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            packageFlight.Id = createDto.Id;
        }

        _context.PackageFlights.Add(packageFlight);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PackageFlightDbModel>(packageFlight.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one PackageFlight
    /// </summary>
    public async Task DeletePackageFlight(PackageFlightWhereUniqueInput uniqueId)
    {
        var packageFlight = await _context.PackageFlights.FindAsync(uniqueId.Id);
        if (packageFlight == null)
        {
            throw new NotFoundException();
        }

        _context.PackageFlights.Remove(packageFlight);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many PackageFlights
    /// </summary>
    public async Task<List<PackageFlight>> PackageFlights(PackageFlightFindManyArgs findManyArgs)
    {
        var packageFlights = await _context
            .PackageFlights.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return packageFlights.ConvertAll(packageFlight => packageFlight.ToDto());
    }

    /// <summary>
    /// Meta data about PackageFlight records
    /// </summary>
    public async Task<MetadataDto> PackageFlightsMeta(PackageFlightFindManyArgs findManyArgs)
    {
        var count = await _context.PackageFlights.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one PackageFlight
    /// </summary>
    public async Task<PackageFlight> PackageFlight(PackageFlightWhereUniqueInput uniqueId)
    {
        var packageFlights = await this.PackageFlights(
            new PackageFlightFindManyArgs
            {
                Where = new PackageFlightWhereInput { Id = uniqueId.Id }
            }
        );
        var packageFlight = packageFlights.FirstOrDefault();
        if (packageFlight == null)
        {
            throw new NotFoundException();
        }

        return packageFlight;
    }

    /// <summary>
    /// Update one PackageFlight
    /// </summary>
    public async Task UpdatePackageFlight(
        PackageFlightWhereUniqueInput uniqueId,
        PackageFlightUpdateInput updateDto
    )
    {
        var packageFlight = updateDto.ToModel(uniqueId);

        _context.Entry(packageFlight).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.PackageFlights.Any(e => e.Id == packageFlight.Id))
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
