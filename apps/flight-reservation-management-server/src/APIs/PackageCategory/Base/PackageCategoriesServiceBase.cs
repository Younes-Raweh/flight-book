using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class PackageCategoriesServiceBase : IPackageCategoriesService
{
    protected readonly FlightReservationManagementDbContext _context;

    public PackageCategoriesServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one PackageCategory
    /// </summary>
    public async Task<PackageCategory> CreatePackageCategory(PackageCategoryCreateInput createDto)
    {
        var packageCategory = new PackageCategoryDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            packageCategory.Id = createDto.Id;
        }

        _context.PackageCategories.Add(packageCategory);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PackageCategoryDbModel>(packageCategory.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one PackageCategory
    /// </summary>
    public async Task DeletePackageCategory(PackageCategoryWhereUniqueInput uniqueId)
    {
        var packageCategory = await _context.PackageCategories.FindAsync(uniqueId.Id);
        if (packageCategory == null)
        {
            throw new NotFoundException();
        }

        _context.PackageCategories.Remove(packageCategory);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many PackageCategories
    /// </summary>
    public async Task<List<PackageCategory>> PackageCategories(
        PackageCategoryFindManyArgs findManyArgs
    )
    {
        var packageCategories = await _context
            .PackageCategories.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return packageCategories.ConvertAll(packageCategory => packageCategory.ToDto());
    }

    /// <summary>
    /// Meta data about PackageCategory records
    /// </summary>
    public async Task<MetadataDto> PackageCategoriesMeta(PackageCategoryFindManyArgs findManyArgs)
    {
        var count = await _context.PackageCategories.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one PackageCategory
    /// </summary>
    public async Task<PackageCategory> PackageCategory(PackageCategoryWhereUniqueInput uniqueId)
    {
        var packageCategories = await this.PackageCategories(
            new PackageCategoryFindManyArgs
            {
                Where = new PackageCategoryWhereInput { Id = uniqueId.Id }
            }
        );
        var packageCategory = packageCategories.FirstOrDefault();
        if (packageCategory == null)
        {
            throw new NotFoundException();
        }

        return packageCategory;
    }

    /// <summary>
    /// Update one PackageCategory
    /// </summary>
    public async Task UpdatePackageCategory(
        PackageCategoryWhereUniqueInput uniqueId,
        PackageCategoryUpdateInput updateDto
    )
    {
        var packageCategory = updateDto.ToModel(uniqueId);

        _context.Entry(packageCategory).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.PackageCategories.Any(e => e.Id == packageCategory.Id))
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
