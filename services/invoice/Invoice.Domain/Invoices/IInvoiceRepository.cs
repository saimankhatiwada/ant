namespace Invoice.Domain.Invoices;
public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(InvoiceId id, CancellationToken cancellationToken = default);
    
    void Add(Invoice invoice);
}
