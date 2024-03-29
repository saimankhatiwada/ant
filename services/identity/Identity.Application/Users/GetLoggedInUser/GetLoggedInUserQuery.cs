using Identity.Application.Abstractions.Messaging;

namespace Identity.Application.Users.GetLoggedInUser;

public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;
