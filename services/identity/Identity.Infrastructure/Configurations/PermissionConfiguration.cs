using Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configurations;
internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(permission => permission.Id);

        builder.HasData(
            Permission.UserReadOwn,
            Permission.UserCollectAllPermission,
            Permission.InvoiceCreation, 
            Permission.InvoiceItemInsertion,
            Permission.InvoiceItemDeletion,
            Permission.InvoiceVerification,
            Permission.InvoiceReportGeneration,
            Permission.InvoiceGet,
            Permission.InvoiceGetAll);
    }
}
