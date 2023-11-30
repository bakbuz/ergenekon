using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

[Authorize]
public class UserController : ApiControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}
