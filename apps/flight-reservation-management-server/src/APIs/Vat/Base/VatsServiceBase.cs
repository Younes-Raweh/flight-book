using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class VatsServiceBase : IVatsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public VatsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Vat
    /// </summary>
    public async Task<Vat> CreateVat(VatCreateInput createDto)
    {
        var vat = new VatDbModel
        {
            CarVatType = createDto.CarVatType,
            CarVatValue = createDto.CarVatValue,
            CreatedAt = createDto.CreatedAt,
            FlightVatType = createDto.FlightVatType,
            FlightVatValue = createDto.FlightVatValue,
            HotelVatType = createDto.HotelVatType,
            HotelVatValue = createDto.HotelVatValue,
            PackageVatType = createDto.PackageVatType,
            PackageVatValue = createDto.PackageVatValue,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            vat.Id = createDto.Id;
        }

        _context.Vats.Add(vat);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<VatDbModel>(vat.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Vat
    /// </summary>
    public async Task DeleteVat(VatWhereUniqueInput uniqueId)
    {
        var vat = await _context.Vats.FindAsync(uniqueId.Id);
        if (vat == null)
        {
            throw new NotFoundException();
        }

        _context.Vats.Remove(vat);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Vats
    /// </summary>
    public async Task<List<Vat>> Vats(VatFindManyArgs findManyArgs)
    {
        var vats = await _context
            .Vats.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return vats.ConvertAll(vat => vat.ToDto());
    }

    /// <summary>
    /// Meta data about Vat records
    /// </summary>
    public async Task<MetadataDto> VatsMeta(VatFindManyArgs findManyArgs)
    {
        var count = await _context.Vats.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Vat
    /// </summary>
    public async Task<Vat> Vat(VatWhereUniqueInput uniqueId)
    {
        var vats = await this.Vats(
            new VatFindManyArgs { Where = new VatWhereInput { Id = uniqueId.Id } }
        );
        var vat = vats.FirstOrDefault();
        if (vat == null)
        {
            throw new NotFoundException();
        }

        return vat;
    }

    /// <summary>
    /// Update one Vat
    /// </summary>
    public async Task UpdateVat(VatWhereUniqueInput uniqueId, VatUpdateInput updateDto)
    {
        var vat = updateDto.ToModel(uniqueId);

        _context.Entry(vat).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Vats.Any(e => e.Id == vat.Id))
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
