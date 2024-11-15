using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IBanksService
{
    /// <summary>
    /// Create one Bank
    /// </summary>
    public Task<Bank> CreateBank(BankCreateInput bank);

    /// <summary>
    /// Delete one Bank
    /// </summary>
    public Task DeleteBank(BankWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Banks
    /// </summary>
    public Task<List<Bank>> Banks(BankFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Bank records
    /// </summary>
    public Task<MetadataDto> BanksMeta(BankFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Bank
    /// </summary>
    public Task<Bank> Bank(BankWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Bank
    /// </summary>
    public Task UpdateBank(BankWhereUniqueInput uniqueId, BankUpdateInput updateDto);
}
