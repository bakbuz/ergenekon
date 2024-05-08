using Ergenekon.Infrastructure.Identity;
using Ergenekon.Infrastructure.Localization;
using FluentEmail.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Ergenekon.Infrastructure;

public interface IMailboxService
{
    Task SendEmailAsync(string recipient, string subject, string htmlMessage, CancellationToken cancellationToken);

    Task SendEmailConfirmationLinkAsync(ApplicationUser user, string callbackUrl, CancellationToken cancellationToken);

    Task SendEmailConfirmationCodeAsync(ApplicationUser user, int code, CancellationToken cancellationToken);

    Task SendPasswordRecoveryEmailAsync(ApplicationUser user, string callbackUrl, CancellationToken cancellationToken);

    Task SendChangeEmailTokenAsync(ApplicationUser user, string newEmail, string callbackUrl, CancellationToken cancellationToken);

    Task SendEmailChangedMessageAsync(ApplicationUser user, string oldEmail, string newEmail, string changePasswordUrl, string rollbackUrl, CancellationToken cancellationToken);
}

public class MailboxService : IMailboxService
{
    private IFluentEmail _fluentEmail;
    private readonly IConfiguration _configuration;
    private readonly ILogger<MailboxService> _logger;

    public MailboxService(IFluentEmail fluentEmail, IConfiguration configuration, ILogger<MailboxService> logger)
    {
        _fluentEmail = fluentEmail;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendEmailAsync(string recipient, string subject, string htmlMessage, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _fluentEmail
              .To(recipient)
              .Subject(subject)
              .Body(htmlMessage, true)
              .SendAsync(cancellationToken);

            if (!response.Successful)
                _logger.LogError("SendEmailAsync Unsuccessful", response.ErrorMessages);
        }
        catch (Exception ex)
        {
            _logger.LogCritical("SendEmailAsync Exception", ex);
        }
    }

    public async Task SendEmailConfirmationLinkAsync(ApplicationUser user, string callbackUrl, CancellationToken cancellationToken)
    {
        var displayName = user.GetDisplayName();
        var emailBody = EmailBodyBuilder.NewInstance()
            .AddTitle(EmailResources.EmailConfirmation_SubjectLink)
            .AddParagraph(string.Format(EmailResources.EmailConfirmation_Body1, displayName))
            .AddParagraph(EmailResources.EmailConfirmation_Body2)
            .AddParagraph(string.Format(EmailResources.EmailConfirmation_Body3_Link, callbackUrl))
            .AddParagraph(EmailResources.EmailConfirmation_Body4)
            .AddFooterInfo()
            .Build();

        await SendEmailAsync(user.Email, EmailResources.EmailConfirmation_SubjectLink, emailBody, cancellationToken);
    }

    public async Task SendEmailConfirmationCodeAsync(ApplicationUser user, int code, CancellationToken cancellationToken)
    {
        var displayName = user.GetDisplayName();
        //var clientUrl = _configuration["AppOptions:ClientUrl"];

        var emailBody = EmailBodyBuilder.NewInstance()
           .AddTitle(EmailResources.EmailConfirmation_SubjectCode)
           .AddParagraph(string.Format(EmailResources.EmailConfirmation_Body1, displayName))
           .AddParagraph(EmailResources.EmailConfirmation_Body2)
           .AddParagraph(string.Format(EmailResources.EmailConfirmation_Body3_Code, code))
           .AddParagraph(EmailResources.EmailConfirmation_Body4)
           .AddFooterInfo()
           .Build();

        await SendEmailAsync(user.Email, EmailResources.EmailConfirmation_SubjectCode, emailBody, cancellationToken);
    }

    public async Task SendPasswordRecoveryEmailAsync(ApplicationUser user, string callbackUrl, CancellationToken cancellationToken)
    {
        var displayName = user.GetDisplayName();

        var emailBody = EmailBodyBuilder.NewInstance()
           .AddTitle(EmailResources.PasswordRecovery_Subject)
           .AddParagraph(string.Format(EmailResources.PasswordRecovery_Body1, displayName))
           .AddParagraph(EmailResources.PasswordRecovery_Body2)
           .AddParagraph(string.Format(EmailResources.PasswordRecovery_Body3, callbackUrl))
           .AddParagraph(EmailResources.PasswordRecovery_Body4)
           .AddParagraph(EmailResources.PasswordRecovery_Body5)
           .AddFooterInfo()
           .Build();

        await SendEmailAsync(user.Email, EmailResources.PasswordRecovery_Subject, emailBody, cancellationToken);
    }

    public async Task SendChangeEmailTokenAsync(ApplicationUser user, string newEmail, string callbackUrl, CancellationToken cancellationToken)
    {
        var displayName = user.GetDisplayName();

        var emailBody = EmailBodyBuilder.NewInstance()
          .AddTitle(EmailResources.EmailChangeRequest_Subject)
          .AddParagraph(string.Format(EmailResources.EmailChangeRequest_Body1, displayName))
          .AddParagraph(EmailResources.EmailChangeRequest_Body2)
          .AddParagraph(string.Format(EmailResources.EmailChangeRequest_Body3, callbackUrl))
          .AddParagraph(EmailResources.EmailChangeRequest_Body4)
          .AddFooterInfo()
          .Build();

        await SendEmailAsync(newEmail, EmailResources.EmailChangeRequest_Subject, emailBody, cancellationToken);
    }

    public async Task SendEmailChangedMessageAsync(ApplicationUser user, string oldEmail, string newEmail, string changePasswordUrl, string rollbackUrl, CancellationToken cancellationToken)
    {
        var displayName = user.GetDisplayName();

        var emailBody = EmailBodyBuilder.NewInstance()
          .AddTitle(EmailResources.EmailChanged_Subject)
          .AddParagraph(string.Format(EmailResources.EmailChanged_Body1, displayName))
          .AddParagraph(string.Format(EmailResources.EmailChanged_Body2, oldEmail, newEmail))
          .AddParagraph(string.Format(EmailResources.EmailChanged_Body3, changePasswordUrl))
          .AddParagraph(EmailResources.EmailChanged_Body4)
          .AddParagraph(string.Format(EmailResources.EmailChanged_Body5, rollbackUrl))
          .AddFooterInfo()
          .Build();

        await SendEmailAsync(oldEmail, EmailResources.EmailChanged_Subject, emailBody, cancellationToken);
    }


}

internal class EmailBodyBuilder
{
    private StringBuilder _builder;

    private EmailBodyBuilder()
    {
        if (_builder == null)
            _builder = new StringBuilder();
    }

    private string Title { get; set; }

    private bool FooterInfo { get; set; }

    internal static EmailBodyBuilder NewInstance()
    {
        return new EmailBodyBuilder();
    }

    internal EmailBodyBuilder AddTitle(string title)
    {
        Title = title;
        return this;
    }

    internal EmailBodyBuilder AddParagraph(string paragraph)
    {
        _builder.AppendFormat("<p>{0}</p>", paragraph);
        return this;
    }

    internal EmailBodyBuilder AddFooterInfo(bool addFooterInformation = true)
    {
        FooterInfo = addFooterInformation;
        return this;
    }

    internal string Build()
    {
        _builder.AppendFormat("<div class=\"mt\"><p>{0}</p></div>", EmailResources.FooterSignature);

        if (FooterInfo)
            _builder.AppendFormat("<div class=\"footer\"><p>{0}</p></div>", EmailResources.FooterInfo);


        var body = _builder.ToString();
        return HtmlLayout.Replace("{TITLE}", Title).Replace("{BODY}", body);
    }

    const string HtmlLayout = @"
<!DOCTYPE html>
<html lang=""en"">
<head>
  <meta charset=""UTF-8"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
  <title>{TITLE}</title>
  <style>
    .body{font:300 14px/18px 'Lucida Grande',Lucida Sans,Lucida Sans Unicode,sans-serif,Arial,Helvetica,Verdana,sans-serif;color:#333}
    .footer{margin-top:15px;border-top:1px solid #eaeaea;text-align:center;font:11px/15px Geneva,Verdana,Arial,Helvetica,sans-serif;color:#888}
    .footer a{color:#08c;text-decoration:none}
    .mt{margin-top:15px}
  </style>
</head>
<body>
{BODY}
</body>
</html>";
}
