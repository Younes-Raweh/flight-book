using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class TravelPackagesServiceBase : ITravelPackagesService
{
    protected readonly FlightReservationManagementDbContext _context;

    public TravelPackagesServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one TravelPackage
    /// </summary>
    public async Task<TravelPackage> CreateTravelPackage(TravelPackageCreateInput createDto)
    {
        var travelPackage = new TravelPackageDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            Token = createDto.Token,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            travelPackage.Id = createDto.Id;
        }

        _context.TravelPackages.Add(travelPackage);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<TravelPackageDbModel>(travelPackage.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one TravelPackage
    /// </summary>
    public async Task DeleteTravelPackage(TravelPackageWhereUniqueInput uniqueId)
    {
        var travelPackage = await _context.TravelPackages.FindAsync(uniqueId.Id);
        if (travelPackage == null)
        {
            throw new NotFoundException();
        }

        _context.TravelPackages.Remove(travelPackage);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many TravelPackages
    /// </summary>
    public async Task<List<TravelPackage>> TravelPackages(TravelPackageFindManyArgs findManyArgs)
    {
        var travelPackages = await _context
            .TravelPackages.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return travelPackages.ConvertAll(travelPackage => travelPackage.ToDto());
    }

    /// <summary>
    /// Meta data about TravelPackage records
    /// </summary>
    public async Task<MetadataDto> TravelPackagesMeta(TravelPackageFindManyArgs findManyArgs)
    {
        var count = await _context.TravelPackages.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one TravelPackage
    /// </summary>
    public async Task<TravelPackage> TravelPackage(TravelPackageWhereUniqueInput uniqueId)
    {
        var travelPackages = await this.TravelPackages(
            new TravelPackageFindManyArgs
            {
                Where = new TravelPackageWhereInput { Id = uniqueId.Id }
            }
        );
        var travelPackage = travelPackages.FirstOrDefault();
        if (travelPackage == null)
        {
            throw new NotFoundException();
        }

        return travelPackage;
    }

    /// <summary>
    /// Update one TravelPackage
    /// </summary>
    public async Task UpdateTravelPackage(
        TravelPackageWhereUniqueInput uniqueId,
        TravelPackageUpdateInput updateDto
    )
    {
        var travelPackage = updateDto.ToModel(uniqueId);

        _context.Entry(travelPackage).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.TravelPackages.Any(e => e.Id == travelPackage.Id))
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
