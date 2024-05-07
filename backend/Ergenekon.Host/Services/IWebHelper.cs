namespace Ergenekon.Host.Services;

public interface IWebHelper
{
    string GetCurrentApplicationUrl();

    string EmailConfirmCallbackUrl(string userId, string code);

    string PasswordRecoveryCallbackUrl(string code);
}
