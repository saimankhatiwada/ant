using Invoice.Domain.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoice.Infrastructure.Configurations;

internal sealed class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.ToTable("invoice_items");
        
        builder.HasKey(invoiceItem => invoiceItem.Id);

        builder.Property(invoiceItem => invoiceItem.Id)
            .HasConversion(id => id.Value, value => new InvoiceItemId(value));

        builder.Property(invoiceItem => invoiceItem.Name)
            .HasConversion(name => name.Value, value => new Name(value));

        builder.Property(invoiceItem => invoiceItem.Quantity)
            .HasConversion(quantity => quantity.Value, value => Quantity.Create(value).Value);

        builder.OwnsOne(invoiceItem => invoiceItem.Money, priceBuilder => priceBuilder.Property(money => money.Currency)
            .HasConversion(currency => currency.Code, code => Currency.FromCode(code)));
    }
}
