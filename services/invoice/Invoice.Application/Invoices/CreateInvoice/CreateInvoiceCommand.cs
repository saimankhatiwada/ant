using Invoice.Application.Abstractions.Messaging;

namespace Invoice.Application.Invoices.CreateInvoice;

public sealed record CreateInvoiceCommand(string InvoiceName) : ICommand<Guid>;
