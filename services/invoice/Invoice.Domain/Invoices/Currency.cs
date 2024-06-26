namespace Invoice.Domain.Invoices;

public sealed record Currency
{
    internal static readonly Currency None = new(string.Empty);
    public static readonly Currency Npr = new("NPR");
    public static readonly Currency Usd = new("USD");
    private Currency(string code) => Code = code;
    public string Code { get; init; }

    public static Currency FromCode(string code) => All.FirstOrDefault(c => c.Code == code) ??
        throw new ApplicationException("The currency code is invalid");

    public static Currency CheckCode(string code) => All.FirstOrDefault(c => c.Code == code) ??
        None;
    public static readonly IReadOnlyCollection<Currency> All = new[]
    {
        Npr,
        Usd
    };
}
