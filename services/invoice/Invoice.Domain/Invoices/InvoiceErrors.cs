using Invoice.Domain.Abstractions;

namespace Invoice.Domain.Invoices;

public static class InvoiceErrors
{
    public static readonly Error InvoiceItemNotFound = new(
        "Invoice.InvoiceItem",
        "The invoice is empty.");

    public static readonly Error InvoiceNotFound = new(
        "Invoice.Found",
        "The invoice doesnot exists.");
}
