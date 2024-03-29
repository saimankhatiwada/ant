namespace Invoice.Application.Invoices.GetAllInvoiceWithItem;
public sealed class InvoiceItemResponse
{
    public Guid InvoiceItemId { get; init; }
    public string Name { get; init; }
    public int Quantity { get; init; }
    public decimal Amount { get; init; }
    public string Currency { get; init; }
}
