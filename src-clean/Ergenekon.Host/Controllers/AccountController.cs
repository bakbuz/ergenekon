using Ergenekon.Application.Authentication.Commands.Login;
using Ergenekon.Application.Authentication.Commands.Register;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

public class AccountController : ApiControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await Mediator.Send(command);
        if (!result.Result.Succeeded)
            return BadRequest(result.Result.Errors);

        return Created("", result.UserId);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await Mediator.Send(command);
        if (!result.Result.Succeeded)
            return BadRequest(result.Result.Errors);

        return Created("", result.UserId);
    }
}
