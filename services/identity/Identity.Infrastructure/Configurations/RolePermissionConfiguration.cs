using Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("role_permissions");

        builder.HasKey(rolePermission => new { rolePermission.RoleId, rolePermission.PermissionId });

        builder.HasData(
            new RolePermission
            {
                RoleId = Role.Maker.Id,
                PermissionId = Permission.UserReadOwn.Id
            },
            new RolePermission
            {
                RoleId = Role.Maker.Id,
                PermissionId = Permission.UserCollectAllPermission.Id
            },
            new RolePermission
            {
                RoleId = Role.Maker.Id,
                PermissionId = Permission.InvoiceCreation.Id
            },
            new RolePermission
            {
                RoleId = Role.Maker.Id,
                PermissionId = Permission.InvoiceItemInsertion.Id
            },
            new RolePermission
            {
                RoleId = Role.Maker.Id,
                PermissionId = Permission.InvoiceItemDeletion.Id
            },
            new RolePermission
            {
                RoleId = Role.Maker.Id,
                PermissionId = Permission.InvoiceGet.Id
            },
            new RolePermission
            {
                RoleId = Role.Maker.Id,
                PermissionId = Permission.InvoiceGetAll.Id
            },
            new RolePermission
            {
                RoleId = Role.Checker.Id,
                PermissionId = Permission.UserReadOwn.Id
            },
            new RolePermission
            {
                RoleId = Role.Checker.Id,
                PermissionId = Permission.UserCollectAllPermission.Id
            },
            new RolePermission
            {
                RoleId = Role.Checker.Id,
                PermissionId = Permission.InvoiceCreation.Id
            },
            new RolePermission
            {
                RoleId = Role.Checker.Id,
                PermissionId = Permission.InvoiceItemInsertion.Id
            },
            new RolePermission
            {
                RoleId = Role.Checker.Id,
                PermissionId = Permission.InvoiceItemDeletion.Id
            },
            new RolePermission
            {
                RoleId = Role.Checker.Id,
                PermissionId = Permission.InvoiceVerification.Id
            },
            new RolePermission
            {
                RoleId = Role.Checker.Id,
                PermissionId = Permission.InvoiceReportGeneration.Id
            },
            new RolePermission
            {
                RoleId = Role.Checker.Id,
                PermissionId = Permission.InvoiceGet.Id
            },
            new RolePermission
            {
                RoleId = Role.Checker.Id,
                PermissionId = Permission.InvoiceGetAll.Id
            }
        );
    }

}
