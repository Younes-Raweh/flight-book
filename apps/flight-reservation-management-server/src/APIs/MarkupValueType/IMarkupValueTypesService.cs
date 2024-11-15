using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IMarkupValueTypesService
{
    /// <summary>
    /// Create one MarkupValueType
    /// </summary>
    public Task<MarkupValueType> CreateMarkupValueType(MarkupValueTypeCreateInput markupvaluetype);

    /// <summary>
    /// Delete one MarkupValueType
    /// </summary>
    public Task DeleteMarkupValueType(MarkupValueTypeWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many MarkupValueTypes
    /// </summary>
    public Task<List<MarkupValueType>> MarkupValueTypes(MarkupValueTypeFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about MarkupValueType records
    /// </summary>
    public Task<MetadataDto> MarkupValueTypesMeta(MarkupValueTypeFindManyArgs findManyArgs);

    /// <summary>
    /// Get one MarkupValueType
    /// </summary>
    public Task<MarkupValueType> MarkupValueType(MarkupValueTypeWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one MarkupValueType
    /// </summary>
    public Task UpdateMarkupValueType(
        MarkupValueTypeWhereUniqueInput uniqueId,
        MarkupValueTypeUpdateInput updateDto
    );
}
