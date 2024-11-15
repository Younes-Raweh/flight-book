using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface ICabinTypesService
{
    /// <summary>
    /// Create one CabinType
    /// </summary>
    public Task<CabinType> CreateCabinType(CabinTypeCreateInput cabintype);

    /// <summary>
    /// Delete one CabinType
    /// </summary>
    public Task DeleteCabinType(CabinTypeWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many CabinTypes
    /// </summary>
    public Task<List<CabinType>> CabinTypes(CabinTypeFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about CabinType records
    /// </summary>
    public Task<MetadataDto> CabinTypesMeta(CabinTypeFindManyArgs findManyArgs);

    /// <summary>
    /// Get one CabinType
    /// </summary>
    public Task<CabinType> CabinType(CabinTypeWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one CabinType
    /// </summary>
    public Task UpdateCabinType(CabinTypeWhereUniqueInput uniqueId, CabinTypeUpdateInput updateDto);
}
