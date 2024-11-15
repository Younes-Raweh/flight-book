using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IOnlinePaymentsService
{
    /// <summary>
    /// Create one OnlinePayment
    /// </summary>
    public Task<OnlinePayment> CreateOnlinePayment(OnlinePaymentCreateInput onlinepayment);

    /// <summary>
    /// Delete one OnlinePayment
    /// </summary>
    public Task DeleteOnlinePayment(OnlinePaymentWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many OnlinePayments
    /// </summary>
    public Task<List<OnlinePayment>> OnlinePayments(OnlinePaymentFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about OnlinePayment records
    /// </summary>
    public Task<MetadataDto> OnlinePaymentsMeta(OnlinePaymentFindManyArgs findManyArgs);

    /// <summary>
    /// Get one OnlinePayment
    /// </summary>
    public Task<OnlinePayment> OnlinePayment(OnlinePaymentWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one OnlinePayment
    /// </summary>
    public Task UpdateOnlinePayment(
        OnlinePaymentWhereUniqueInput uniqueId,
        OnlinePaymentUpdateInput updateDto
    );
}
