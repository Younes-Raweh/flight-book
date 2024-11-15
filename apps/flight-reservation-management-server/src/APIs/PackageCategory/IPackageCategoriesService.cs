using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IPackageCategoriesService
{
    /// <summary>
    /// Create one PackageCategory
    /// </summary>
    public Task<PackageCategory> CreatePackageCategory(PackageCategoryCreateInput packagecategory);

    /// <summary>
    /// Delete one PackageCategory
    /// </summary>
    public Task DeletePackageCategory(PackageCategoryWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many PackageCategories
    /// </summary>
    public Task<List<PackageCategory>> PackageCategories(PackageCategoryFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about PackageCategory records
    /// </summary>
    public Task<MetadataDto> PackageCategoriesMeta(PackageCategoryFindManyArgs findManyArgs);

    /// <summary>
    /// Get one PackageCategory
    /// </summary>
    public Task<PackageCategory> PackageCategory(PackageCategoryWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one PackageCategory
    /// </summary>
    public Task UpdatePackageCategory(
        PackageCategoryWhereUniqueInput uniqueId,
        PackageCategoryUpdateInput updateDto
    );
}
