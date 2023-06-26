namespace Ergenekon.Application.Common.Interfaces;

public interface IWebHelper
{
    string GetCurrentApplicationUrl();

    string GetEmailConfirmCallbackUrl(string userId, string code);
}
