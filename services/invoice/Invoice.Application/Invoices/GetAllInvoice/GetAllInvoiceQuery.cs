using Invoice.Application.Abstractions.Messaging;

namespace Invoice.Application.Invoices.GetAllInvoice;

public sealed record GetAllInvoiceQuery() : IQuery<IReadOnlyList<InvoiceResponse>>;
