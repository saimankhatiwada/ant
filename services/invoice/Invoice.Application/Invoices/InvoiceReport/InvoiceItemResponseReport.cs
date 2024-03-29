namespace Invoice.Application.Invoices.InvoiceReport;

public sealed class InvoiceItemResponseReport
{
    public Guid InvoiceItemId { get; init; }
    public string Name { get; init; }
    public int Quantity { get; init; }
    public decimal Amount { get; init; }
    public string Currency { get; init; }
}
