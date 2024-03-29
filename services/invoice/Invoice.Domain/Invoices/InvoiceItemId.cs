namespace Invoice.Domain.Invoices;

public record InvoiceItemId(Guid Value)
{
    public static InvoiceItemId New() => new(Guid.NewGuid());
}
