using Invoice.Application.Abstractions.Messaging;

namespace Invoice.Application.Invoices.RemoveInvoiceItem;
public sealed record RemoveInvoiceItemCommand(Guid InvoiceId, Guid InvoicetemId) : ICommand;
