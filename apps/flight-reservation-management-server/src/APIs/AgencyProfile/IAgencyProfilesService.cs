using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IAgencyProfilesService
{
    /// <summary>
    /// Create one AgencyProfile
    /// </summary>
    public Task<AgencyProfile> CreateAgencyProfile(AgencyProfileCreateInput agencyprofile);

    /// <summary>
    /// Delete one AgencyProfile
    /// </summary>
    public Task DeleteAgencyProfile(AgencyProfileWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many AgencyProfiles
    /// </summary>
    public Task<List<AgencyProfile>> AgencyProfiles(AgencyProfileFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about AgencyProfile records
    /// </summary>
    public Task<MetadataDto> AgencyProfilesMeta(AgencyProfileFindManyArgs findManyArgs);

    /// <summary>
    /// Get one AgencyProfile
    /// </summary>
    public Task<AgencyProfile> AgencyProfile(AgencyProfileWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one AgencyProfile
    /// </summary>
    public Task UpdateAgencyProfile(
        AgencyProfileWhereUniqueInput uniqueId,
        AgencyProfileUpdateInput updateDto
    );
}
