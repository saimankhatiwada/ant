namespace Invoice.Domain.Invoices;

public record InvoiceId(Guid Value)
{
    public static InvoiceId New() => new(Guid.NewGuid());
}
