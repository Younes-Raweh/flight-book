using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class PackageAttractionsServiceBase : IPackageAttractionsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public PackageAttractionsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one PackageAttraction
    /// </summary>
    public async Task<PackageAttraction> CreatePackageAttraction(
        PackageAttractionCreateInput createDto
    )
    {
        var packageAttraction = new PackageAttractionDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            packageAttraction.Id = createDto.Id;
        }

        _context.PackageAttractions.Add(packageAttraction);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PackageAttractionDbModel>(packageAttraction.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one PackageAttraction
    /// </summary>
    public async Task DeletePackageAttraction(PackageAttractionWhereUniqueInput uniqueId)
    {
        var packageAttraction = await _context.PackageAttractions.FindAsync(uniqueId.Id);
        if (packageAttraction == null)
        {
            throw new NotFoundException();
        }

        _context.PackageAttractions.Remove(packageAttraction);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many PackageAttractions
    /// </summary>
    public async Task<List<PackageAttraction>> PackageAttractions(
        PackageAttractionFindManyArgs findManyArgs
    )
    {
        var packageAttractions = await _context
            .PackageAttractions.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return packageAttractions.ConvertAll(packageAttraction => packageAttraction.ToDto());
    }

    /// <summary>
    /// Meta data about PackageAttraction records
    /// </summary>
    public async Task<MetadataDto> PackageAttractionsMeta(
        PackageAttractionFindManyArgs findManyArgs
    )
    {
        var count = await _context.PackageAttractions.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one PackageAttraction
    /// </summary>
    public async Task<PackageAttraction> PackageAttraction(
        PackageAttractionWhereUniqueInput uniqueId
    )
    {
        var packageAttractions = await this.PackageAttractions(
            new PackageAttractionFindManyArgs
            {
                Where = new PackageAttractionWhereInput { Id = uniqueId.Id }
            }
        );
        var packageAttraction = packageAttractions.FirstOrDefault();
        if (packageAttraction == null)
        {
            throw new NotFoundException();
        }

        return packageAttraction;
    }

    /// <summary>
    /// Update one PackageAttraction
    /// </summary>
    public async Task UpdatePackageAttraction(
        PackageAttractionWhereUniqueInput uniqueId,
        PackageAttractionUpdateInput updateDto
    )
    {
        var packageAttraction = updateDto.ToModel(uniqueId);

        _context.Entry(packageAttraction).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.PackageAttractions.Any(e => e.Id == packageAttraction.Id))
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
