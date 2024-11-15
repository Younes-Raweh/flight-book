using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IVisaApplicationsService
{
    /// <summary>
    /// Create one VisaApplication
    /// </summary>
    public Task<VisaApplication> CreateVisaApplication(VisaApplicationCreateInput visaapplication);

    /// <summary>
    /// Delete one VisaApplication
    /// </summary>
    public Task DeleteVisaApplication(VisaApplicationWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many VisaApplications
    /// </summary>
    public Task<List<VisaApplication>> VisaApplications(VisaApplicationFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about VisaApplication records
    /// </summary>
    public Task<MetadataDto> VisaApplicationsMeta(VisaApplicationFindManyArgs findManyArgs);

    /// <summary>
    /// Get one VisaApplication
    /// </summary>
    public Task<VisaApplication> VisaApplication(VisaApplicationWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one VisaApplication
    /// </summary>
    public Task UpdateVisaApplication(
        VisaApplicationWhereUniqueInput uniqueId,
        VisaApplicationUpdateInput updateDto
    );
}
