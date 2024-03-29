using Invoice.Domain.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoice.Infrastructure.Configurations;

internal sealed class InvoiceConfiguration : IEntityTypeConfiguration<Domain.Invoices.Invoice>
{
    public void Configure(EntityTypeBuilder<Domain.Invoices.Invoice> builder)
    {
        builder.ToTable("invoices");
        
        builder.HasKey(invoice => invoice.Id);

        builder.Property(invoice => invoice.Id)
            .HasConversion(id => id.Value, value => new InvoiceId(value));

        builder.Property(invoice => invoice.InvoiceName)
            .HasConversion(invoiceName => invoiceName.Value, value => new InvoiceName(value));

        builder.Property(invoice => invoice.IsVerified)
            .HasConversion(isVerified => isVerified.Value, value => new IsVerified(value));

        builder.HasMany(invoice => invoice.invoiceItems)
            .WithOne()
            .HasForeignKey(ii => ii.InvoiceId);
    }
}
