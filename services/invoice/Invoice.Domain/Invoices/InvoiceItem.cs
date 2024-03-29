using Invoice.Domain.Abstractions;

namespace Invoice.Domain.Invoices;

public sealed class InvoiceItem : Entity<InvoiceItemId>
{
    private InvoiceItem(InvoiceItemId id, InvoiceId invoiceId, Name name, Quantity quantity, Money money) : base(id)
    {
        InvoiceId = invoiceId;
        Name = name;
        Quantity = quantity;
        Money = money;
    }

    private InvoiceItem()
    {
    }

    public InvoiceId InvoiceId { get; private set; }
    public Name Name { get; private set; }
    public Quantity Quantity { get; private set; } 
    public Money Money { get; private set; }

    public static InvoiceItem Create(InvoiceId invoiceId, Name name, Quantity quantity, Money money)
    {
        var invoiceItem = new InvoiceItem(
            InvoiceItemId.New(), 
            invoiceId, 
            name, 
            quantity,
            money);

        return invoiceItem;
    }
}
