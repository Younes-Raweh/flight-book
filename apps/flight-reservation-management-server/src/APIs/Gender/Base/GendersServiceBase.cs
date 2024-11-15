using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class GendersServiceBase : IGendersService
{
    protected readonly FlightReservationManagementDbContext _context;

    public GendersServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Gender
    /// </summary>
    public async Task<Gender> CreateGender(GenderCreateInput createDto)
    {
        var gender = new GenderDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            gender.Id = createDto.Id;
        }

        _context.Genders.Add(gender);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<GenderDbModel>(gender.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Gender
    /// </summary>
    public async Task DeleteGender(GenderWhereUniqueInput uniqueId)
    {
        var gender = await _context.Genders.FindAsync(uniqueId.Id);
        if (gender == null)
        {
            throw new NotFoundException();
        }

        _context.Genders.Remove(gender);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Genders
    /// </summary>
    public async Task<List<Gender>> Genders(GenderFindManyArgs findManyArgs)
    {
        var genders = await _context
            .Genders.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return genders.ConvertAll(gender => gender.ToDto());
    }

    /// <summary>
    /// Meta data about Gender records
    /// </summary>
    public async Task<MetadataDto> GendersMeta(GenderFindManyArgs findManyArgs)
    {
        var count = await _context.Genders.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Gender
    /// </summary>
    public async Task<Gender> Gender(GenderWhereUniqueInput uniqueId)
    {
        var genders = await this.Genders(
            new GenderFindManyArgs { Where = new GenderWhereInput { Id = uniqueId.Id } }
        );
        var gender = genders.FirstOrDefault();
        if (gender == null)
        {
            throw new NotFoundException();
        }

        return gender;
    }

    /// <summary>
    /// Update one Gender
    /// </summary>
    public async Task UpdateGender(GenderWhereUniqueInput uniqueId, GenderUpdateInput updateDto)
    {
        var gender = updateDto.ToModel(uniqueId);

        _context.Entry(gender).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Genders.Any(e => e.Id == gender.Id))
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
