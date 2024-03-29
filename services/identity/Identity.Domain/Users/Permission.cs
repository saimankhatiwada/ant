namespace Identity.Domain.Users;
public sealed class Permission
{
    public static readonly Permission UserReadOwn = new(1, "users:readown");
    public static readonly Permission UserCollectAllPermission = new(2, "users:collectallpermission");
    public static readonly Permission InvoiceCreation = new(3, "invoices:creation");
    public static readonly Permission InvoiceItemInsertion = new(4, "invoices:iteminsertion");
    public static readonly Permission InvoiceItemDeletion = new(5, "invoices:itemdeletion");
    public static readonly Permission InvoiceVerification = new(6, "invoices:verification");
    public static readonly Permission InvoiceReportGeneration = new(7, "invoices:reportgeneration");
    public static readonly Permission InvoiceGet = new(8, "invoices:get");
    public static readonly Permission InvoiceGetAll = new(9, "invoices:getall");

    private Permission(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }

    public string Name { get; init; }
}
