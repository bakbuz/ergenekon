using Ergenekon.Infrastructure.Identity;
using Ergenekon.Infrastructure.Identity.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ergenekon.Host.Controllers;

public class AuthController : ApiControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet("token")]
    public async Task<IActionResult> Token()
    {
        (IdentityResult result, string userId) = await _authenticationService.LoginAsync(new LoginRequest(Ergenekon.Infrastructure.Identity.IdentityConstants.DefaultUserEmail, Ergenekon.Infrastructure.Identity.IdentityConstants.DefaultUserPass));
        if (!result.Succeeded)
            return BadRequest(new ResponseErrors(result));

        var tokenValues = _authenticationService.CreateToken(userId);
        return Ok(tokenValues);
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TokenValues))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseErrors))]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var validateResult = new RegisterRequestValidator().Validate(request);
        if (!validateResult.IsValid)
            return BadRequest(new ResponseErrors(validateResult));

        (IdentityResult result, string userId) = await _authenticationService.RegisterAsync(request, cancellationToken);
        if (!result.Succeeded)
            return BadRequest(new ResponseErrors(result));

        var tokenValues = _authenticationService.CreateToken(userId);
        return Created("", tokenValues);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenValues))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseErrors))]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        var validateResult = new LoginRequestValidator().Validate(request);
        if (!validateResult.IsValid)
            return BadRequest(new ResponseErrors(validateResult));

        (IdentityResult result, string userId) = await _authenticationService.LoginAsync(request);
        if (!result.Succeeded)
            return BadRequest(new ResponseErrors(result));

        var tokenValues = _authenticationService.CreateToken(userId);
        return Ok(tokenValues);
    }

    [HttpPost("password-recovery")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseErrors))]
    public async Task<IActionResult> PasswordRecoveryAsync([FromBody] PasswordRecoveryRequest request, CancellationToken cancellationToken)
    {
        var validateResult = new PasswordRecoveryRequestValidator().Validate(request);
        if (!validateResult.IsValid)
            return BadRequest(new ResponseErrors(validateResult));

        var result = await _authenticationService.PasswordRecoveryAsync(request, cancellationToken);
        if (!result.Succeeded)
            return BadRequest(new ResponseErrors(result));

        return Ok(new ResponseMessage("Belirttiğiniz e-posta adresinize parola sıfırlama bağlantısı içeren bir e-posta gönderildi. Lütfen e-posta adresinizi denetleyin."));
    }

    [HttpPost("password-reset")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseErrors))]
    public async Task<IActionResult> PasswordResetAsync([FromBody] PasswordResetRequest request)
    {
        var validateResult = new PasswordResetRequestValidator().Validate(request);
        if (!validateResult.IsValid)
            return BadRequest(new ResponseErrors(validateResult));

        var result = await _authenticationService.PasswordResetAsync(request);
        if (!result.Succeeded)
            return BadRequest(new ResponseErrors(result));

        return Ok(new ResponseMessage("Parolanız başarıyla sıfırlandı. Lütfen oturum açın."));
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmailAsync([FromQuery] ConfirmEmailRequest request)
    {
        var validateResult = new ConfirmEmailRequestValidator().Validate(request);
        if (!validateResult.IsValid)
            return BadRequest(new ResponseErrors(validateResult));

        var result = await _authenticationService.ConfirmEmailAsync(request);
        if (!result.Succeeded)
            return BadRequest(new ResponseErrors(result));

        return Ok(new ResponseMessage("E-posta adresiniz başarıyla doğrulandı"));
    }

    [HttpGet("confirm-email-change")]
    public async Task<IActionResult> ConfirmEmailChangeAsync([FromQuery] ConfirmEmailChangeRequest request)
    {
        var validateResult = new ConfirmEmailChangeRequestValidator().Validate(request);
        if (!validateResult.IsValid)
            return BadRequest(new ResponseErrors(validateResult));

        var result = await _authenticationService.ConfirmEmailChangeAsync(request);
        if (!result.Succeeded)
            return BadRequest(new ResponseErrors(result));

        return Ok(new ResponseMessage("E-posta adresiniz başarıyla değiştirildi"));
    }
}

public class ResponseMessage
{
    public ResponseMessage(string message)
    {
        Message = message;
    }
    public string Message { get; set; }
}

public class ResponseError
{
    public ResponseError(string message, string? code = null)
    {
        Code = code;
        Message = message;
    }

    public string? Code { get; set; }

    public string Message { get; set; }
}

public class ResponseErrors
{
    public ResponseErrors(string message, string? code = null)
    {
        Errors = new ResponseError[] { new ResponseError(message, code) };
    }

    public ResponseErrors(IdentityResult result)
    {
        Errors = result.Errors.Select(s => new ResponseError(s.Description, s.Code)).ToArray();
    }

    public ResponseErrors(ModelStateDictionary modelState)
    {
        Errors = modelState.SelectMany(s => s.Value.Errors.Select(e => new ResponseError(e.ErrorMessage))).ToArray();
    }

    public ResponseErrors(ValidationResult validateResult)
    {
        Errors = validateResult.Errors.Select(s => new ResponseError(s.ErrorMessage, s.ErrorCode)).ToArray();
    }

    public ResponseError[] Errors { get; set; }
}