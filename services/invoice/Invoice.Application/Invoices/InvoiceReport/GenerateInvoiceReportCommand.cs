using Invoice.Application.Abstractions.Messaging;

namespace Invoice.Application.Invoices.InvoiceReport;
public sealed record GenerateInvoiceReportCommand(Guid InvoiceId) : ICommand<PdfDocument>;
