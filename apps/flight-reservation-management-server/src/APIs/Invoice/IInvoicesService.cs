using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface IInvoicesService
{
    /// <summary>
    /// Create one Invoice
    /// </summary>
    public Task<Invoice> CreateInvoice(InvoiceCreateInput invoice);

    /// <summary>
    /// Delete one Invoice
    /// </summary>
    public Task DeleteInvoice(InvoiceWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Invoices
    /// </summary>
    public Task<List<Invoice>> Invoices(InvoiceFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Invoice records
    /// </summary>
    public Task<MetadataDto> InvoicesMeta(InvoiceFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Invoice
    /// </summary>
    public Task<Invoice> Invoice(InvoiceWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Invoice
    /// </summary>
    public Task UpdateInvoice(InvoiceWhereUniqueInput uniqueId, InvoiceUpdateInput updateDto);
}
