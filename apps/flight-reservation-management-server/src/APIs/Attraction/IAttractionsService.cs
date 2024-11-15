using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IAttractionsService
{
    /// <summary>
    /// Create one Attraction
    /// </summary>
    public Task<Attraction> CreateAttraction(AttractionCreateInput attraction);

    /// <summary>
    /// Delete one Attraction
    /// </summary>
    public Task DeleteAttraction(AttractionWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Attractions
    /// </summary>
    public Task<List<Attraction>> Attractions(AttractionFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Attraction records
    /// </summary>
    public Task<MetadataDto> AttractionsMeta(AttractionFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Attraction
    /// </summary>
    public Task<Attraction> Attraction(AttractionWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Attraction
    /// </summary>
    public Task UpdateAttraction(
        AttractionWhereUniqueInput uniqueId,
        AttractionUpdateInput updateDto
    );
}
