namespace Invoice.Application.Invoices.GetAllInvoice;
public sealed class InvoiceResponse
{
    public Guid Id { get; init; }
    public string InvoiceName { get; init; }
    public bool IsVerified { get; init; }
}
