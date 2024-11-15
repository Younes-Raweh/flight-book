using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class BankPaymentsServiceBase : IBankPaymentsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public BankPaymentsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one BankPayment
    /// </summary>
    public async Task<BankPayment> CreateBankPayment(BankPaymentCreateInput createDto)
    {
        var bankPayment = new BankPaymentDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            bankPayment.Id = createDto.Id;
        }

        _context.BankPayments.Add(bankPayment);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<BankPaymentDbModel>(bankPayment.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one BankPayment
    /// </summary>
    public async Task DeleteBankPayment(BankPaymentWhereUniqueInput uniqueId)
    {
        var bankPayment = await _context.BankPayments.FindAsync(uniqueId.Id);
        if (bankPayment == null)
        {
            throw new NotFoundException();
        }

        _context.BankPayments.Remove(bankPayment);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many BankPayments
    /// </summary>
    public async Task<List<BankPayment>> BankPayments(BankPaymentFindManyArgs findManyArgs)
    {
        var bankPayments = await _context
            .BankPayments.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return bankPayments.ConvertAll(bankPayment => bankPayment.ToDto());
    }

    /// <summary>
    /// Meta data about BankPayment records
    /// </summary>
    public async Task<MetadataDto> BankPaymentsMeta(BankPaymentFindManyArgs findManyArgs)
    {
        var count = await _context.BankPayments.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one BankPayment
    /// </summary>
    public async Task<BankPayment> BankPayment(BankPaymentWhereUniqueInput uniqueId)
    {
        var bankPayments = await this.BankPayments(
            new BankPaymentFindManyArgs { Where = new BankPaymentWhereInput { Id = uniqueId.Id } }
        );
        var bankPayment = bankPayments.FirstOrDefault();
        if (bankPayment == null)
        {
            throw new NotFoundException();
        }

        return bankPayment;
    }

    /// <summary>
    /// Update one BankPayment
    /// </summary>
    public async Task UpdateBankPayment(
        BankPaymentWhereUniqueInput uniqueId,
        BankPaymentUpdateInput updateDto
    )
    {
        var bankPayment = updateDto.ToModel(uniqueId);

        _context.Entry(bankPayment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.BankPayments.Any(e => e.Id == bankPayment.Id))
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
