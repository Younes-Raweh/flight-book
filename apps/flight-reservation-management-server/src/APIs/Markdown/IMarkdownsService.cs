using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IMarkdownsService
{
    /// <summary>
    /// Create one Markdown
    /// </summary>
    public Task<Markdown> CreateMarkdown(MarkdownCreateInput markdown);

    /// <summary>
    /// Delete one Markdown
    /// </summary>
    public Task DeleteMarkdown(MarkdownWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Markdowns
    /// </summary>
    public Task<List<Markdown>> Markdowns(MarkdownFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Markdown records
    /// </summary>
    public Task<MetadataDto> MarkdownsMeta(MarkdownFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Markdown
    /// </summary>
    public Task<Markdown> Markdown(MarkdownWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Markdown
    /// </summary>
    public Task UpdateMarkdown(MarkdownWhereUniqueInput uniqueId, MarkdownUpdateInput updateDto);
}
