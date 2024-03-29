namespace Invoice.Application.Invoices.GetAllInvoiceWithItem;

public sealed class InvoiceWithItemResponse
{
    public Guid Id { get; init; }
    public string InvoiceName { get; init; }
    public bool IsVerified { get; init; }
    public List<InvoiceItemResponse> InvoiceItems { get; init; } = [];
}
