using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IBankDetailsService
{
    /// <summary>
    /// Create one BankDetail
    /// </summary>
    public Task<BankDetail> CreateBankDetail(BankDetailCreateInput bankdetail);

    /// <summary>
    /// Delete one BankDetail
    /// </summary>
    public Task DeleteBankDetail(BankDetailWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many BankDetails
    /// </summary>
    public Task<List<BankDetail>> BankDetails(BankDetailFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about BankDetail records
    /// </summary>
    public Task<MetadataDto> BankDetailsMeta(BankDetailFindManyArgs findManyArgs);

    /// <summary>
    /// Get one BankDetail
    /// </summary>
    public Task<BankDetail> BankDetail(BankDetailWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one BankDetail
    /// </summary>
    public Task UpdateBankDetail(
        BankDetailWhereUniqueInput uniqueId,
        BankDetailUpdateInput updateDto
    );
}
