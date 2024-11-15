using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IPayLatersService
{
    /// <summary>
    /// Create one PayLater
    /// </summary>
    public Task<PayLater> CreatePayLater(PayLaterCreateInput paylater);

    /// <summary>
    /// Delete one PayLater
    /// </summary>
    public Task DeletePayLater(PayLaterWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many PayLaters
    /// </summary>
    public Task<List<PayLater>> PayLaters(PayLaterFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about PayLater records
    /// </summary>
    public Task<MetadataDto> PayLatersMeta(PayLaterFindManyArgs findManyArgs);

    /// <summary>
    /// Get one PayLater
    /// </summary>
    public Task<PayLater> PayLater(PayLaterWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one PayLater
    /// </summary>
    public Task UpdatePayLater(PayLaterWhereUniqueInput uniqueId, PayLaterUpdateInput updateDto);
}
