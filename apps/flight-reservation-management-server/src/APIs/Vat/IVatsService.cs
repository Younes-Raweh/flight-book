using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IVatsService
{
    /// <summary>
    /// Create one Vat
    /// </summary>
    public Task<Vat> CreateVat(VatCreateInput vat);

    /// <summary>
    /// Delete one Vat
    /// </summary>
    public Task DeleteVat(VatWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Vats
    /// </summary>
    public Task<List<Vat>> Vats(VatFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Vat records
    /// </summary>
    public Task<MetadataDto> VatsMeta(VatFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Vat
    /// </summary>
    public Task<Vat> Vat(VatWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Vat
    /// </summary>
    public Task UpdateVat(VatWhereUniqueInput uniqueId, VatUpdateInput updateDto);
}
