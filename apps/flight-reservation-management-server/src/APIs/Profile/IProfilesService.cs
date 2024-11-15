using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IProfilesService
{
    /// <summary>
    /// Create one Profile
    /// </summary>
    public Task<Profile> CreateProfile(ProfileCreateInput profile);

    /// <summary>
    /// Delete one Profile
    /// </summary>
    public Task DeleteProfile(ProfileWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Profiles
    /// </summary>
    public Task<List<Profile>> Profiles(ProfileFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Profile records
    /// </summary>
    public Task<MetadataDto> ProfilesMeta(ProfileFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Profile
    /// </summary>
    public Task<Profile> Profile(ProfileWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Profile
    /// </summary>
    public Task UpdateProfile(ProfileWhereUniqueInput uniqueId, ProfileUpdateInput updateDto);
}
