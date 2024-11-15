using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IEmailSubscribersService
{
    /// <summary>
    /// Create one EmailSubscriber
    /// </summary>
    public Task<EmailSubscriber> CreateEmailSubscriber(EmailSubscriberCreateInput emailsubscriber);

    /// <summary>
    /// Delete one EmailSubscriber
    /// </summary>
    public Task DeleteEmailSubscriber(EmailSubscriberWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many EmailSubscribers
    /// </summary>
    public Task<List<EmailSubscriber>> EmailSubscribers(EmailSubscriberFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about EmailSubscriber records
    /// </summary>
    public Task<MetadataDto> EmailSubscribersMeta(EmailSubscriberFindManyArgs findManyArgs);

    /// <summary>
    /// Get one EmailSubscriber
    /// </summary>
    public Task<EmailSubscriber> EmailSubscriber(EmailSubscriberWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one EmailSubscriber
    /// </summary>
    public Task UpdateEmailSubscriber(
        EmailSubscriberWhereUniqueInput uniqueId,
        EmailSubscriberUpdateInput updateDto
    );
}
