using Invoice.Application.Abstractions.Messaging;

namespace Invoice.Application.Invoices.AddInvoiceItem;

public sealed record AddInvoiceItemCommand(Guid InvoiceId, string Name, int Quantity, decimal Amount, string Currency) : ICommand;
