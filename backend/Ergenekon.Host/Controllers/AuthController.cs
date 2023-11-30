using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

public class AuthController : ApiControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}
