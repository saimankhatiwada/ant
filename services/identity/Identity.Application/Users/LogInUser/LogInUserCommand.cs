using Identity.Application.Abstractions.Messaging;

namespace Identity.Application.Users.LogInUser;
public sealed record LogInUserCommand(string Email, string Password) : ICommand<AccessTokenResponse>;
