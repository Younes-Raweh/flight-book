using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IMarkupsService
{
    /// <summary>
    /// Create one Markup
    /// </summary>
    public Task<Markup> CreateMarkup(MarkupCreateInput markup);

    /// <summary>
    /// Delete one Markup
    /// </summary>
    public Task DeleteMarkup(MarkupWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Markups
    /// </summary>
    public Task<List<Markup>> Markups(MarkupFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Markup records
    /// </summary>
    public Task<MetadataDto> MarkupsMeta(MarkupFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Markup
    /// </summary>
    public Task<Markup> Markup(MarkupWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Markup
    /// </summary>
    public Task UpdateMarkup(MarkupWhereUniqueInput uniqueId, MarkupUpdateInput updateDto);
}
