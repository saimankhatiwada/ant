namespace Invoice.Api.Utils;

internal static class Permission
{
    public const string InvoiceCreation = "invoices:creation";
    public const string InvoiceItemInsertion = "invoices:iteminsertion";
    public const string InvoiceItemDeletion = "invoices:itemdeletion";
    public const string InvoiceVerification = "invoices:verification";
    public const string InvoiceReportGeneration = "invoices:reportgeneration";
    public const string InvoiceGetAll = "invoices:getall";
}
