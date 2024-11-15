using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class PackageTypesServiceBase : IPackageTypesService
{
    protected readonly FlightReservationManagementDbContext _context;

    public PackageTypesServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one PackageType
    /// </summary>
    public async Task<PackageType> CreatePackageType(PackageTypeCreateInput createDto)
    {
        var packageType = new PackageTypeDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            packageType.Id = createDto.Id;
        }

        _context.PackageTypes.Add(packageType);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PackageTypeDbModel>(packageType.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one PackageType
    /// </summary>
    public async Task DeletePackageType(PackageTypeWhereUniqueInput uniqueId)
    {
        var packageType = await _context.PackageTypes.FindAsync(uniqueId.Id);
        if (packageType == null)
        {
            throw new NotFoundException();
        }

        _context.PackageTypes.Remove(packageType);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many PackageTypes
    /// </summary>
    public async Task<List<PackageType>> PackageTypes(PackageTypeFindManyArgs findManyArgs)
    {
        var packageTypes = await _context
            .PackageTypes.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return packageTypes.ConvertAll(packageType => packageType.ToDto());
    }

    /// <summary>
    /// Meta data about PackageType records
    /// </summary>
    public async Task<MetadataDto> PackageTypesMeta(PackageTypeFindManyArgs findManyArgs)
    {
        var count = await _context.PackageTypes.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one PackageType
    /// </summary>
    public async Task<PackageType> PackageType(PackageTypeWhereUniqueInput uniqueId)
    {
        var packageTypes = await this.PackageTypes(
            new PackageTypeFindManyArgs { Where = new PackageTypeWhereInput { Id = uniqueId.Id } }
        );
        var packageType = packageTypes.FirstOrDefault();
        if (packageType == null)
        {
            throw new NotFoundException();
        }

        return packageType;
    }

    /// <summary>
    /// Update one PackageType
    /// </summary>
    public async Task UpdatePackageType(
        PackageTypeWhereUniqueInput uniqueId,
        PackageTypeUpdateInput updateDto
    )
    {
        var packageType = updateDto.ToModel(uniqueId);

        _context.Entry(packageType).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.PackageTypes.Any(e => e.Id == packageType.Id))
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
