using System.Data;
using Dapper;
using Invoice.Application.Abstractions.Data;
using Invoice.Application.Abstractions.Messaging;
using Invoice.Domain.Abstractions;
using Razor.Templating.Core;

namespace Invoice.Application.Invoices.InvoiceReport;
internal sealed class GenerateInvoiceReportCommandHandler : ICommandHandler<GenerateInvoiceReportCommand, PdfDocument>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GenerateInvoiceReportCommandHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task<Result<PdfDocument>> Handle(GenerateInvoiceReportCommand request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        Dictionary<Guid, InvoiceWithItemResponseReport> invoiceDictionary = [];

        await connection
            .QueryAsync<InvoiceWithItemResponseReport, InvoiceItemResponseReport, InvoiceWithItemResponseReport>(
                """
                SELECT
                    i.id AS Id,
                    i.invoice_name AS InvoiceName,
                    i.is_verified AS IsVerified,
                    ii.id AS InvoiceItemId,
                    ii.name AS Name,
                    ii.quantity AS Quantity,
                    ii.money_amount AS Amount,
                    ii.money_currency AS Currency
                FROM invoices i
                JOIN invoice_items ii ON i.id = ii.invoice_id
                WHERE i.id = @InvoiceId
                """,
                (invoice, invoiceItem) =>
                {
                    if (invoiceDictionary.TryGetValue(invoice.Id, out InvoiceWithItemResponseReport? existinginvoice))
                    {
                        invoice = existinginvoice;
                    }
                    else
                    {
                        invoiceDictionary.Add(invoice.Id, invoice);
                    }

                    invoice.InvoiceItems.Add(invoiceItem);

                    return invoice;
                },
                new
                {
                    request.InvoiceId
                },
                splitOn: "InvoiceItemId");

        InvoiceWithItemResponseReport invoiceResponse = invoiceDictionary[request.InvoiceId];

        string html = await RazorTemplateEngine.RenderAsync("Views/InvoiceReport.cshtml", invoiceResponse);

        var renderer = new ChromePdfRenderer();

        PdfDocument pdfDocument = await renderer.RenderHtmlAsPdfAsync(html);

        return pdfDocument;
    }
}
