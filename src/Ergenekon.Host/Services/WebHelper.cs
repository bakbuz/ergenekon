using Ergenekon.Application.Common.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;

namespace Ergenekon.Host.Services;

public class WebHelper : IWebHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public WebHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
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

    public string GetEmailConfirmCallbackUrl(string userId, string code)
    {
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        return HtmlEncoder.Default.Encode($"{GetCurrentApplicationUrl()}/api/account/confirm-email?userId={userId}&code={code}");
    }
}
