namespace Invoice.Application.Abstractions.Authentication;
public interface ITokenContext
{
    string AccessToken { get; }
}
