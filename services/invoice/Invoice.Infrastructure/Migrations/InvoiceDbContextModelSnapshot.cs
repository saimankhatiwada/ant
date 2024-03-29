﻿// <auto-generated />
using System;
using Invoice.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Invoice.Infrastructure.Migrations
{
    [DbContext(typeof(InvoiceDbContext))]
    partial class InvoiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Invoice.Domain.Invoices.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("InvoiceName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("invoice_name");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("boolean")
                        .HasColumnName("is_verified");

                    b.HasKey("Id")
                        .HasName("pk_invoices");

                    b.ToTable("invoices", (string)null);
                });

            modelBuilder.Entity("Invoice.Domain.Invoices.InvoiceItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uuid")
                        .HasColumnName("invoice_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.HasKey("Id")
                        .HasName("pk_invoice_items");

                    b.HasIndex("InvoiceId")
                        .HasDatabaseName("ix_invoice_items_invoice_id");

                    b.ToTable("invoice_items", (string)null);
                });

            modelBuilder.Entity("Invoice.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("outbox_messages", (string)null);
                });

            modelBuilder.Entity("Invoice.Domain.Invoices.InvoiceItem", b =>
                {
                    b.HasOne("Invoice.Domain.Invoices.Invoice", null)
                        .WithMany("invoiceItems")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_invoice_items_invoices_invoice_id");

                    b.OwnsOne("Invoice.Domain.Invoices.Money", "Money", b1 =>
                        {
                            b1.Property<Guid>("InvoiceItemId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("money_amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("money_currency");

                            b1.HasKey("InvoiceItemId");

                            b1.ToTable("invoice_items");

                            b1.WithOwner()
                                .HasForeignKey("InvoiceItemId")
                                .HasConstraintName("fk_invoice_items_invoice_items_id");
                        });

                    b.Navigation("Money")
                        .IsRequired();
                });

            modelBuilder.Entity("Invoice.Domain.Invoices.Invoice", b =>
                {
                    b.Navigation("invoiceItems");
                });
#pragma warning restore 612, 618
        }
    }
}
