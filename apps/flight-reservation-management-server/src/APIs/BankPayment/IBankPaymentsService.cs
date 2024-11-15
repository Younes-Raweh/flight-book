using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IBankPaymentsService
{
    /// <summary>
    /// Create one BankPayment
    /// </summary>
    public Task<BankPayment> CreateBankPayment(BankPaymentCreateInput bankpayment);

    /// <summary>
    /// Delete one BankPayment
    /// </summary>
    public Task DeleteBankPayment(BankPaymentWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many BankPayments
    /// </summary>
    public Task<List<BankPayment>> BankPayments(BankPaymentFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about BankPayment records
    /// </summary>
    public Task<MetadataDto> BankPaymentsMeta(BankPaymentFindManyArgs findManyArgs);

    /// <summary>
    /// Get one BankPayment
    /// </summary>
    public Task<BankPayment> BankPayment(BankPaymentWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one BankPayment
    /// </summary>
    public Task UpdateBankPayment(
        BankPaymentWhereUniqueInput uniqueId,
        BankPaymentUpdateInput updateDto
    );
}
