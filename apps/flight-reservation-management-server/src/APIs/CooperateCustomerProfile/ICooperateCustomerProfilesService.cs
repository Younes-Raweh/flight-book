using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface ICooperateCustomerProfilesService
{
    /// <summary>
    /// Create one CooperateCustomerProfile
    /// </summary>
    public Task<CooperateCustomerProfile> CreateCooperateCustomerProfile(
        CooperateCustomerProfileCreateInput cooperatecustomerprofile
    );

    /// <summary>
    /// Delete one CooperateCustomerProfile
    /// </summary>
    public Task DeleteCooperateCustomerProfile(CooperateCustomerProfileWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many CooperateCustomerProfiles
    /// </summary>
    public Task<List<CooperateCustomerProfile>> CooperateCustomerProfiles(
        CooperateCustomerProfileFindManyArgs findManyArgs
    );

    /// <summary>
    /// Meta data about CooperateCustomerProfile records
    /// </summary>
    public Task<MetadataDto> CooperateCustomerProfilesMeta(
        CooperateCustomerProfileFindManyArgs findManyArgs
    );

    /// <summary>
    /// Get one CooperateCustomerProfile
    /// </summary>
    public Task<CooperateCustomerProfile> CooperateCustomerProfile(
        CooperateCustomerProfileWhereUniqueInput uniqueId
    );

    /// <summary>
    /// Update one CooperateCustomerProfile
    /// </summary>
    public Task UpdateCooperateCustomerProfile(
        CooperateCustomerProfileWhereUniqueInput uniqueId,
        CooperateCustomerProfileUpdateInput updateDto
    );
}
