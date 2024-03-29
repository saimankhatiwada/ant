using Identity.Application.Abstractions.Authentication;
using Identity.Application.Abstractions.Messaging;
using Identity.Domain.Abstractions;
using Identity.Domain.Users;

namespace Identity.Application.Users.CollectUserPermission;

internal sealed class CollectUserPermissionCommandHandler : ICommandHandler<CollectUserPermissionCommand, HashSet<string>>
{
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepository;

    public CollectUserPermissionCommandHandler(IUserContext userContext, IUserRepository userRepository)
    {
        _userContext = userContext;
        _userRepository = userRepository;
    }
    public async Task<Result<HashSet<string>>> Handle(CollectUserPermissionCommand request, CancellationToken cancellationToken)
    {
        ICollection<Permission> permissions = await _userRepository.GetAllUserPermissions(new UserId(_userContext.UserId), cancellationToken);

        return permissions.Select(p => p.Name).ToHashSet();
    }

}
