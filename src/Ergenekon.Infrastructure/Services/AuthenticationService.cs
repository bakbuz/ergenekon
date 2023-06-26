using Ergenekon.Application.Authentication.Commands.Login;
using Ergenekon.Application.Authentication.Commands.Register;
using Ergenekon.Application.Authentication.Services;
using Ergenekon.Application.Authentication.Shared;
using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Ergenekon.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMailboxService _mailboxService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IConfiguration _configuration;
    private readonly IWebHelper _webHelper;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IMailboxService mailboxService,
        ICurrentUserService currentUserService,
        IConfiguration configuration,
        IWebHelper webHelper,
        ILogger<AuthenticationService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mailboxService = mailboxService;
        _currentUserService = currentUserService;
        _configuration = configuration;
        _webHelper = webHelper;
        _logger = logger;
    }


    public async Task<string> GetUserIdAsync(string email)
    {
        var userId = await _userManager.FindByEmailAsync(email);

        return userId.Id;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = command.Username,
            Email = command.Email,
            CreatedAt = DateTime.Now
        };

        var result = await _userManager.CreateAsync(user, command.Password);
        if (!result.Succeeded)
            return result;

        _logger.LogInformation("User created a new account with password.");

        // send email
        var userId = await _userManager.GetUserIdAsync(user);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var callbackUrl = _webHelper.GetEmailConfirmCallbackUrl(userId, code);

        await _mailboxService.SendEmailConfirmationLinkAsync(user, callbackUrl, cancellationToken);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> LoginAsync(LoginCommand command)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "Kullanıcı bulunamadı" });

        var result = await _signInManager.PasswordSignInAsync(user, command.Password, false, lockoutOnFailure: true);
        if (result.Succeeded)
        {
            _logger.LogInformation("User logged in.");

            return IdentityResult.Success;
        }
        if (result.RequiresTwoFactor)
        {
            return IdentityResult.Failed(new IdentityError { Description = "İki adımlı güvenlik doğrulaması ile devam etmeniz gerekmektedir.", Code = "Need2FA" });
        }
        if (result.IsLockedOut)
        {
            _logger.LogWarning("User account locked out.");
            return IdentityResult.Failed(new IdentityError { Description = "Çok fazla giriş denemesinden sonra hesabınız kilitlendi. Bu kilitlenme durumu bir saat kadar geçerli olur, daha sonra ortadan kalkar.", Code = "Locked" });
        }
        else
        {
            return IdentityResult.Failed(new IdentityError { Description = "E-posta adresi ya da parola yanlış." });
        }
    }

    public TokenValues CreateToken(string userId)
    {
        var issuer = _configuration["JwtOptions:Issuer"];
        var audience = _configuration["JwtOptions:Audience"];
        var secretKey = _configuration["JwtOptions:SecretKey"];
        var expireDays = Convert.ToInt16(_configuration["JwtOptions:ExpireDays"]);

        var expires = DateTime.Now.AddDays(expireDays);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<System.Security.Claims.Claim>()
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, userId)
            };
        var securityToken = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            notBefore: DateTime.Now,
            signingCredentials: signingCredentials);

        var securityTokenHandler = new JwtSecurityTokenHandler();
        var accessToken = securityTokenHandler.WriteToken(securityToken);


        var token = new TokenValues();
        token.AccessToken = accessToken;
        token.Expires = expires;

        // refresh token
        byte[] numbers = new byte[32];
        using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(numbers);
        }

        token.RefreshToken = Convert.ToBase64String(numbers);

        return token;
    }

}
