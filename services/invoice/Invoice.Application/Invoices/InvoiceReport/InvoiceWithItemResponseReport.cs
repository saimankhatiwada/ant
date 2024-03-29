namespace Invoice.Application.Invoices.InvoiceReport;

public sealed class InvoiceWithItemResponseReport
{
    public Guid Id { get; init; }
    public string InvoiceName { get; init; }
    public bool IsVerified { get; init; }
    public List<InvoiceItemResponseReport> InvoiceItems { get; init; } = [];
}
