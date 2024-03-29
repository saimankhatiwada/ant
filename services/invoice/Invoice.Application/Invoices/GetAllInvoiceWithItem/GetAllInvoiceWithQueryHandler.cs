using System.Data;
using Dapper;
using Invoice.Application.Abstractions.Data;
using Invoice.Application.Abstractions.Messaging;
using Invoice.Domain.Abstractions;

namespace Invoice.Application.Invoices.GetAllInvoiceWithItem;
internal sealed class GetAllInvoiceWithQueryHandler : IQueryHandler<GetAllInvoiceWithItemQuery, IReadOnlyList<InvoiceWithItemResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    public GetAllInvoiceWithQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task<Result<IReadOnlyList<InvoiceWithItemResponse>>> Handle(GetAllInvoiceWithItemQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        Dictionary<Guid, InvoiceWithItemResponse> invoiceDictionary = [];

        await connection
            .QueryAsync<InvoiceWithItemResponse, InvoiceItemResponse, InvoiceWithItemResponse>(
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
                """,
                (invoice, invoiceItem) =>
                {
                    if (invoiceDictionary.TryGetValue(invoice.Id, out InvoiceWithItemResponse? existinginvoice))
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
                splitOn: "InvoiceItemId");

        return invoiceDictionary.Values.ToList().AsReadOnly();
    }

}
