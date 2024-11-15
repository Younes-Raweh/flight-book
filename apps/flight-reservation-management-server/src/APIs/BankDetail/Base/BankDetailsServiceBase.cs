using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class BankDetailsServiceBase : IBankDetailsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public BankDetailsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one BankDetail
    /// </summary>
    public async Task<BankDetail> CreateBankDetail(BankDetailCreateInput createDto)
    {
        var bankDetail = new BankDetailDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            bankDetail.Id = createDto.Id;
        }

        _context.BankDetails.Add(bankDetail);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<BankDetailDbModel>(bankDetail.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one BankDetail
    /// </summary>
    public async Task DeleteBankDetail(BankDetailWhereUniqueInput uniqueId)
    {
        var bankDetail = await _context.BankDetails.FindAsync(uniqueId.Id);
        if (bankDetail == null)
        {
            throw new NotFoundException();
        }

        _context.BankDetails.Remove(bankDetail);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many BankDetails
    /// </summary>
    public async Task<List<BankDetail>> BankDetails(BankDetailFindManyArgs findManyArgs)
    {
        var bankDetails = await _context
            .BankDetails.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return bankDetails.ConvertAll(bankDetail => bankDetail.ToDto());
    }

    /// <summary>
    /// Meta data about BankDetail records
    /// </summary>
    public async Task<MetadataDto> BankDetailsMeta(BankDetailFindManyArgs findManyArgs)
    {
        var count = await _context.BankDetails.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one BankDetail
    /// </summary>
    public async Task<BankDetail> BankDetail(BankDetailWhereUniqueInput uniqueId)
    {
        var bankDetails = await this.BankDetails(
            new BankDetailFindManyArgs { Where = new BankDetailWhereInput { Id = uniqueId.Id } }
        );
        var bankDetail = bankDetails.FirstOrDefault();
        if (bankDetail == null)
        {
            throw new NotFoundException();
        }

        return bankDetail;
    }

    /// <summary>
    /// Update one BankDetail
    /// </summary>
    public async Task UpdateBankDetail(
        BankDetailWhereUniqueInput uniqueId,
        BankDetailUpdateInput updateDto
    )
    {
        var bankDetail = updateDto.ToModel(uniqueId);

        _context.Entry(bankDetail).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.BankDetails.Any(e => e.Id == bankDetail.Id))
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
