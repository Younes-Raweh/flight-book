using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class OnlinePaymentsServiceBase : IOnlinePaymentsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public OnlinePaymentsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one OnlinePayment
    /// </summary>
    public async Task<OnlinePayment> CreateOnlinePayment(OnlinePaymentCreateInput createDto)
    {
        var onlinePayment = new OnlinePaymentDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            onlinePayment.Id = createDto.Id;
        }

        _context.OnlinePayments.Add(onlinePayment);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<OnlinePaymentDbModel>(onlinePayment.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one OnlinePayment
    /// </summary>
    public async Task DeleteOnlinePayment(OnlinePaymentWhereUniqueInput uniqueId)
    {
        var onlinePayment = await _context.OnlinePayments.FindAsync(uniqueId.Id);
        if (onlinePayment == null)
        {
            throw new NotFoundException();
        }

        _context.OnlinePayments.Remove(onlinePayment);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many OnlinePayments
    /// </summary>
    public async Task<List<OnlinePayment>> OnlinePayments(OnlinePaymentFindManyArgs findManyArgs)
    {
        var onlinePayments = await _context
            .OnlinePayments.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return onlinePayments.ConvertAll(onlinePayment => onlinePayment.ToDto());
    }

    /// <summary>
    /// Meta data about OnlinePayment records
    /// </summary>
    public async Task<MetadataDto> OnlinePaymentsMeta(OnlinePaymentFindManyArgs findManyArgs)
    {
        var count = await _context.OnlinePayments.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one OnlinePayment
    /// </summary>
    public async Task<OnlinePayment> OnlinePayment(OnlinePaymentWhereUniqueInput uniqueId)
    {
        var onlinePayments = await this.OnlinePayments(
            new OnlinePaymentFindManyArgs
            {
                Where = new OnlinePaymentWhereInput { Id = uniqueId.Id }
            }
        );
        var onlinePayment = onlinePayments.FirstOrDefault();
        if (onlinePayment == null)
        {
            throw new NotFoundException();
        }

        return onlinePayment;
    }

    /// <summary>
    /// Update one OnlinePayment
    /// </summary>
    public async Task UpdateOnlinePayment(
        OnlinePaymentWhereUniqueInput uniqueId,
        OnlinePaymentUpdateInput updateDto
    )
    {
        var onlinePayment = updateDto.ToModel(uniqueId);

        _context.Entry(onlinePayment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.OnlinePayments.Any(e => e.Id == onlinePayment.Id))
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
