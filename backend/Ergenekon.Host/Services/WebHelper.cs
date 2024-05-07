using Ergenekon.Host.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;

namespace Ergenekon.Infrastructure.Services;

public class WebHelper : IWebHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public WebHelper(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public string? GetCurrentIpAddress()
    {
        return _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
    }

    public string GetCurrentApplicationUrl()
    {
        if (_httpContextAccessor.HttpContext == null)
            return string.Empty;

        bool useSSL = _httpContextAccessor.HttpContext.Request.Scheme == "https";

        //try to get host from the request HOST header
        var hostHeader = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Host];
        if (StringValues.IsNullOrEmpty(hostHeader))
            return string.Empty;

        //add scheme to the URL
        return $"{(useSSL ? Uri.UriSchemeHttps : Uri.UriSchemeHttp)}{Uri.SchemeDelimiter}{hostHeader.FirstOrDefault()}";
    }

    public string EmailConfirmCallbackUrl(string userId, string code)
    {
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        return HtmlEncoder.Default.Encode($"{GetCurrentApplicationUrl()}/api/account/confirm-email?userId={userId}&code={code}");
    }

    public string PasswordRecoveryCallbackUrl(string code)
    {
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        return HtmlEncoder.Default.Encode($"{_configuration["AppOptions:ClientUrl"]}/{_configuration["AppOptions:ClientResetPasswordPath"]}?code={code}");
    }

}
