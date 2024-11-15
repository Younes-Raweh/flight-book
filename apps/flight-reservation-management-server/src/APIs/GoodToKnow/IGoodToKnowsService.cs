using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IGoodToKnowsService
{
    /// <summary>
    /// Create one GoodToKnow
    /// </summary>
    public Task<GoodToKnow> CreateGoodToKnow(GoodToKnowCreateInput goodtoknow);

    /// <summary>
    /// Delete one GoodToKnow
    /// </summary>
    public Task DeleteGoodToKnow(GoodToKnowWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many GoodToKnows
    /// </summary>
    public Task<List<GoodToKnow>> GoodToKnows(GoodToKnowFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about GoodToKnow records
    /// </summary>
    public Task<MetadataDto> GoodToKnowsMeta(GoodToKnowFindManyArgs findManyArgs);

    /// <summary>
    /// Get one GoodToKnow
    /// </summary>
    public Task<GoodToKnow> GoodToKnow(GoodToKnowWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one GoodToKnow
    /// </summary>
    public Task UpdateGoodToKnow(
        GoodToKnowWhereUniqueInput uniqueId,
        GoodToKnowUpdateInput updateDto
    );
}
