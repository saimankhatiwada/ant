using Identity.Application.Abstractions.Messaging;

namespace Identity.Application.Users.CollectUserPermission;
public sealed record CollectUserPermissionCommand() : ICommand<HashSet<string>>;
