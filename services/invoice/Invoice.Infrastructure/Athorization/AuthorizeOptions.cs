namespace Invoice.Infrastructure.Athorization;
public sealed class AuthorizeOptions
{
    public const string Name = "Authorize";
    public string BaseUrl { get; set; } = string.Empty;
}
