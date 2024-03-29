namespace Invoice.Api.Invoices;

public sealed record AddInvoiceItemRequest(string Name, int Quantity, decimal Amount, string Currency);
