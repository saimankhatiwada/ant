using Identity.Application.Abstractions.Messaging;

namespace Identity.Application.Users.RegisterUser;
public sealed record RegisterUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Password,
    string Role) : ICommand<Guid>;
