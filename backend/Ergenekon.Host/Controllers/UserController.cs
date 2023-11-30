using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.Users.Queries.GetUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

[Authorize]
public class UserController : ApiControllerBase
{
    private readonly ICurrentUser _currentUser;

    public UserController(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await Mediator.Send(new GetUserQuery(_currentUser.Id)));
    }
}
