namespace Ergenekon.Application.Common.Interfaces;

public interface IWebHelper
{
    string GetCurrentApplicationUrl();

    string EmailConfirmCallbackUrl(string userId, string code);

    string PasswordRecoveryCallbackUrl(string code);
}
