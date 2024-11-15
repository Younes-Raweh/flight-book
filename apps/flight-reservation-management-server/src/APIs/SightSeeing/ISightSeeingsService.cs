using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface ISightSeeingsService
{
    /// <summary>
    /// Create one SightSeeing
    /// </summary>
    public Task<SightSeeing> CreateSightSeeing(SightSeeingCreateInput sightseeing);

    /// <summary>
    /// Delete one SightSeeing
    /// </summary>
    public Task DeleteSightSeeing(SightSeeingWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many SightSeeings
    /// </summary>
    public Task<List<SightSeeing>> SightSeeings(SightSeeingFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about SightSeeing records
    /// </summary>
    public Task<MetadataDto> SightSeeingsMeta(SightSeeingFindManyArgs findManyArgs);

    /// <summary>
    /// Get one SightSeeing
    /// </summary>
    public Task<SightSeeing> SightSeeing(SightSeeingWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one SightSeeing
    /// </summary>
    public Task UpdateSightSeeing(
        SightSeeingWhereUniqueInput uniqueId,
        SightSeeingUpdateInput updateDto
    );
}
