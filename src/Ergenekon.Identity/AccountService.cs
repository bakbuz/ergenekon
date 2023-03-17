using Ergenekon.Common;
using Ergenekon.Common.Messages;
using Ergenekon.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Encodings.Web;

namespace Ergenekon.Identity
{
    public interface IAccountService
    {
    }

    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly AppSettings _appSettings;
        private readonly ILogger<AccountService> _logger;

        public AccountService(SignInManager<User> signInManager, UserManager<User> userManager, IEmailSender emailSender, IOptions<AppSettings> appSettings, ILogger<AccountService> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = Url.Page(_appSettings.ClientUrl + "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code },
                        protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(request.Email, "Confirm your email",
                      $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return RedirectToPage("RegisterConfirmation", new { email = request.Email });
                }
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }

                return Ok();
            }

            return BadRequest(result.Errors);
        }
    }
}