using Invoice.Domain.Abstractions;

namespace Invoice.Domain.Invoices;
public sealed class Invoice : Entity<InvoiceId>
{
    private readonly List<InvoiceItem> _invoiceItems = new();

    private Invoice(InvoiceId id, InvoiceName invoiceName, IsVerified isVerified) : base(id)
    {
        InvoiceName = invoiceName;
        IsVerified = isVerified;
    }

    private Invoice()
    {
    }

    public InvoiceName InvoiceName { get; private set; }
    public IsVerified IsVerified { get; private set; }

    public IReadOnlyList<InvoiceItem> invoiceItems => _invoiceItems.ToList();

    public static Invoice Create(InvoiceName invoiceName)
    {
        var invoice = new Invoice(InvoiceId.New(), invoiceName, new IsVerified(false));

        return invoice;
    }

    public Result AddInvoiceItem(Name name, Quantity quantity, Money money)
    {
        var invoiceItem = InvoiceItem.Create(
            Id,
            name,
            quantity,
            money);

        _invoiceItems.Add(invoiceItem);

        return Result.Success();
    }

    public Result RemoveCartItem(InvoiceItemId invoiceItemId)
    {
        InvoiceItem? invoiceItem = _invoiceItems.Find(ci => ci.Id == invoiceItemId);

        if (invoiceItem is null)
        {
            return Result.Failure(InvoiceErrors.InvoiceItemNotFound);
        }

        _invoiceItems.Remove(invoiceItem);

        return Result.Success();
    }

    public void SetIsVerified(IsVerified isVerified)
    {
        IsVerified = isVerified;
    }
}
