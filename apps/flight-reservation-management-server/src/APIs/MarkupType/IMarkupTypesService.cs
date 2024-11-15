using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IMarkupTypesService
{
    /// <summary>
    /// Create one MarkupType
    /// </summary>
    public Task<MarkupType> CreateMarkupType(MarkupTypeCreateInput markuptype);

    /// <summary>
    /// Delete one MarkupType
    /// </summary>
    public Task DeleteMarkupType(MarkupTypeWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many MarkupTypes
    /// </summary>
    public Task<List<MarkupType>> MarkupTypes(MarkupTypeFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about MarkupType records
    /// </summary>
    public Task<MetadataDto> MarkupTypesMeta(MarkupTypeFindManyArgs findManyArgs);

    /// <summary>
    /// Get one MarkupType
    /// </summary>
    public Task<MarkupType> MarkupType(MarkupTypeWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one MarkupType
    /// </summary>
    public Task UpdateMarkupType(
        MarkupTypeWhereUniqueInput uniqueId,
        MarkupTypeUpdateInput updateDto
    );
}
