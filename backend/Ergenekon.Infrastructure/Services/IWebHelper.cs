using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ergenekon.Infrastructure.Services;

public interface IWebHelper
{
    string GetCurrentApplicationUrl();

    string EmailConfirmCallbackUrl(string userId, string code);

    string PasswordRecoveryCallbackUrl(string code);
}
