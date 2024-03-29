using Invoice.Domain.Invoices;

namespace Invoice.Infrastructure.Repositories;

internal sealed class InvoiceRepository : Repository<Domain.Invoices.Invoice, InvoiceId>, IInvoiceRepository
{
    public InvoiceRepository(InvoiceDbContext dbContext) : base(dbContext)
    {
    }
}
