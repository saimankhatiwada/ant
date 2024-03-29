using System.Data;
using Dapper;
using Invoice.Application.Abstractions.Data;
using Invoice.Application.Abstractions.Messaging;
using Invoice.Domain.Abstractions;

namespace Invoice.Application.Invoices.GetAllInvoice;
internal sealed class GetAllInvoiceQueryHandler : IQueryHandler<GetAllInvoiceQuery, IReadOnlyList<InvoiceResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    public GetAllInvoiceQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task<Result<IReadOnlyList<InvoiceResponse>>> Handle(GetAllInvoiceQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        string sql = """
        SELECT
            id AS Id,
            invoice_name AS InvoiceName,
            is_verified AS IsVerified
        FROM invoices
        """;

        IEnumerable<InvoiceResponse> invoicesResponse = await connection.QueryAsync<InvoiceResponse>(sql, cancellationToken);

        return invoicesResponse.ToList().AsReadOnly();
    }

}
