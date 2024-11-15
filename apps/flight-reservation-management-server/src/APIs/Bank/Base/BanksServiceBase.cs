using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class BanksServiceBase : IBanksService
{
    protected readonly FlightReservationManagementDbContext _context;

    public BanksServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Bank
    /// </summary>
    public async Task<Bank> CreateBank(BankCreateInput createDto)
    {
        var bank = new BankDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            bank.Id = createDto.Id;
        }

        _context.Banks.Add(bank);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<BankDbModel>(bank.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Bank
    /// </summary>
    public async Task DeleteBank(BankWhereUniqueInput uniqueId)
    {
        var bank = await _context.Banks.FindAsync(uniqueId.Id);
        if (bank == null)
        {
            throw new NotFoundException();
        }

        _context.Banks.Remove(bank);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Banks
    /// </summary>
    public async Task<List<Bank>> Banks(BankFindManyArgs findManyArgs)
    {
        var banks = await _context
            .Banks.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return banks.ConvertAll(bank => bank.ToDto());
    }

    /// <summary>
    /// Meta data about Bank records
    /// </summary>
    public async Task<MetadataDto> BanksMeta(BankFindManyArgs findManyArgs)
    {
        var count = await _context.Banks.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Bank
    /// </summary>
    public async Task<Bank> Bank(BankWhereUniqueInput uniqueId)
    {
        var banks = await this.Banks(
            new BankFindManyArgs { Where = new BankWhereInput { Id = uniqueId.Id } }
        );
        var bank = banks.FirstOrDefault();
        if (bank == null)
        {
            throw new NotFoundException();
        }

        return bank;
    }

    /// <summary>
    /// Update one Bank
    /// </summary>
    public async Task UpdateBank(BankWhereUniqueInput uniqueId, BankUpdateInput updateDto)
    {
        var bank = updateDto.ToModel(uniqueId);

        _context.Entry(bank).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Banks.Any(e => e.Id == bank.Id))
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
