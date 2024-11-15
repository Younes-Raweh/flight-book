using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface ITitlesService
{
    /// <summary>
    /// Create one Title
    /// </summary>
    public Task<Title> CreateTitle(TitleCreateInput title);

    /// <summary>
    /// Delete one Title
    /// </summary>
    public Task DeleteTitle(TitleWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Titles
    /// </summary>
    public Task<List<Title>> Titles(TitleFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Title records
    /// </summary>
    public Task<MetadataDto> TitlesMeta(TitleFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Title
    /// </summary>
    public Task<Title> Title(TitleWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Title
    /// </summary>
    public Task UpdateTitle(TitleWhereUniqueInput uniqueId, TitleUpdateInput updateDto);
}
