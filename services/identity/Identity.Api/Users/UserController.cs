using Asp.Versioning;
using Identity.Api.Utils;
using Identity.Application.Users.CollectUserPermission;
using Identity.Application.Users.LogInUser;
using Identity.Application.Users.RegisterUser;
using Identity.Domain.Abstractions;
using Identity.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Users;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/user")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.FirstName,
            request.LastName,
            request.Password,
            request.Role);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LogIn(
        LogInUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LogInUserCommand(request.Email, request.Password);

        Result<AccessTokenResponse> result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : Unauthorized(result.Error);
    }

    [HasPermission(Permission.UserCollectAllPermission)]
    [HttpGet("all-permission")]
    public async Task<IActionResult> CollectAllUserPermissions(CancellationToken cancellationToken)
    {
        var command = new CollectUserPermissionCommand();

        Result<HashSet<string>> result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

}

