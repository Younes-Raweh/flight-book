using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class PayLatersServiceBase : IPayLatersService
{
    protected readonly FlightReservationManagementDbContext _context;

    public PayLatersServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one PayLater
    /// </summary>
    public async Task<PayLater> CreatePayLater(PayLaterCreateInput createDto)
    {
        var payLater = new PayLaterDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            payLater.Id = createDto.Id;
        }

        _context.PayLaters.Add(payLater);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PayLaterDbModel>(payLater.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one PayLater
    /// </summary>
    public async Task DeletePayLater(PayLaterWhereUniqueInput uniqueId)
    {
        var payLater = await _context.PayLaters.FindAsync(uniqueId.Id);
        if (payLater == null)
        {
            throw new NotFoundException();
        }

        _context.PayLaters.Remove(payLater);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many PayLaters
    /// </summary>
    public async Task<List<PayLater>> PayLaters(PayLaterFindManyArgs findManyArgs)
    {
        var payLaters = await _context
            .PayLaters.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return payLaters.ConvertAll(payLater => payLater.ToDto());
    }

    /// <summary>
    /// Meta data about PayLater records
    /// </summary>
    public async Task<MetadataDto> PayLatersMeta(PayLaterFindManyArgs findManyArgs)
    {
        var count = await _context.PayLaters.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one PayLater
    /// </summary>
    public async Task<PayLater> PayLater(PayLaterWhereUniqueInput uniqueId)
    {
        var payLaters = await this.PayLaters(
            new PayLaterFindManyArgs { Where = new PayLaterWhereInput { Id = uniqueId.Id } }
        );
        var payLater = payLaters.FirstOrDefault();
        if (payLater == null)
        {
            throw new NotFoundException();
        }

        return payLater;
    }

    /// <summary>
    /// Update one PayLater
    /// </summary>
    public async Task UpdatePayLater(
        PayLaterWhereUniqueInput uniqueId,
        PayLaterUpdateInput updateDto
    )
    {
        var payLater = updateDto.ToModel(uniqueId);

        _context.Entry(payLater).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.PayLaters.Any(e => e.Id == payLater.Id))
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
