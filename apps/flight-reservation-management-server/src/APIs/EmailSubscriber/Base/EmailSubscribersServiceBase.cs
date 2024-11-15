using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class EmailSubscribersServiceBase : IEmailSubscribersService
{
    protected readonly FlightReservationManagementDbContext _context;

    public EmailSubscribersServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one EmailSubscriber
    /// </summary>
    public async Task<EmailSubscriber> CreateEmailSubscriber(EmailSubscriberCreateInput createDto)
    {
        var emailSubscriber = new EmailSubscriberDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            emailSubscriber.Id = createDto.Id;
        }

        _context.EmailSubscribers.Add(emailSubscriber);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<EmailSubscriberDbModel>(emailSubscriber.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one EmailSubscriber
    /// </summary>
    public async Task DeleteEmailSubscriber(EmailSubscriberWhereUniqueInput uniqueId)
    {
        var emailSubscriber = await _context.EmailSubscribers.FindAsync(uniqueId.Id);
        if (emailSubscriber == null)
        {
            throw new NotFoundException();
        }

        _context.EmailSubscribers.Remove(emailSubscriber);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many EmailSubscribers
    /// </summary>
    public async Task<List<EmailSubscriber>> EmailSubscribers(
        EmailSubscriberFindManyArgs findManyArgs
    )
    {
        var emailSubscribers = await _context
            .EmailSubscribers.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return emailSubscribers.ConvertAll(emailSubscriber => emailSubscriber.ToDto());
    }

    /// <summary>
    /// Meta data about EmailSubscriber records
    /// </summary>
    public async Task<MetadataDto> EmailSubscribersMeta(EmailSubscriberFindManyArgs findManyArgs)
    {
        var count = await _context.EmailSubscribers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one EmailSubscriber
    /// </summary>
    public async Task<EmailSubscriber> EmailSubscriber(EmailSubscriberWhereUniqueInput uniqueId)
    {
        var emailSubscribers = await this.EmailSubscribers(
            new EmailSubscriberFindManyArgs
            {
                Where = new EmailSubscriberWhereInput { Id = uniqueId.Id }
            }
        );
        var emailSubscriber = emailSubscribers.FirstOrDefault();
        if (emailSubscriber == null)
        {
            throw new NotFoundException();
        }

        return emailSubscriber;
    }

    /// <summary>
    /// Update one EmailSubscriber
    /// </summary>
    public async Task UpdateEmailSubscriber(
        EmailSubscriberWhereUniqueInput uniqueId,
        EmailSubscriberUpdateInput updateDto
    )
    {
        var emailSubscriber = updateDto.ToModel(uniqueId);

        _context.Entry(emailSubscriber).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.EmailSubscribers.Any(e => e.Id == emailSubscriber.Id))
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
