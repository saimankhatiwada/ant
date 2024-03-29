using Invoice.Application.Abstractions.Messaging;

namespace Invoice.Application.Invoices.GetAllInvoiceWithItem;

public sealed record GetAllInvoiceWithItemQuery() : IQuery<IReadOnlyList<InvoiceWithItemResponse>>;
