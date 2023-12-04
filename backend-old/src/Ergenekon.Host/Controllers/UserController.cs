using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.Users.GetCurrentUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

[Authorize]
public class UserController : ApiControllerBase
{
    private readonly ICurrentUser _currentUser;
    private readonly IIdentityService _identityService;

    public UserController(ICurrentUser currentUser, IIdentityService identityService)
    {
        _currentUser = currentUser;
        _identityService = identityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await Mediator.Send(new GetCurrentUserQuery()));
    }
}
