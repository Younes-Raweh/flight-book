using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IGendersService
{
    /// <summary>
    /// Create one Gender
    /// </summary>
    public Task<Gender> CreateGender(GenderCreateInput gender);

    /// <summary>
    /// Delete one Gender
    /// </summary>
    public Task DeleteGender(GenderWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Genders
    /// </summary>
    public Task<List<Gender>> Genders(GenderFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Gender records
    /// </summary>
    public Task<MetadataDto> GendersMeta(GenderFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Gender
    /// </summary>
    public Task<Gender> Gender(GenderWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Gender
    /// </summary>
    public Task UpdateGender(GenderWhereUniqueInput uniqueId, GenderUpdateInput updateDto);
}
