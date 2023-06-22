using Ergenekon.Application.Authentication.Commands.Login;
using Ergenekon.Application.Authentication.Commands.Register;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

[Route("api/[controller]/[action]")]
public class AuthController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterCommand command)
    {
        var result = await Mediator.Send(command);
        if (!result.Result.Succeeded)
            return BadRequest(result.Result.Errors);

        return Created("", result.UserId);
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command)
    {
        var result = await Mediator.Send(command);
        if (!result.Result.Succeeded)
            return BadRequest(result.Result.Errors);

        return Created("", result.UserId);
    }
}
