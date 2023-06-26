using Ergenekon.Application.Common.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;

namespace Ergenekon.Host.Services;

public class WebHelper : IWebHelper
{
    public string GetCurrentApplicationUrl()
    {
        return "";
    }

    public string GetEmailConfirmCallbackUrl(string userId, string code)
    {
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        return HtmlEncoder.Default.Encode($"{GetCurrentApplicationUrl()}/api/account/confirm-email?userId={userId}&code={code}");
    }
}
