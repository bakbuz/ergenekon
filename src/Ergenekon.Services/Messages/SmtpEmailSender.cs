using Ergenekon.Common.Messages;

namespace Ergenekon.Services.Messages
{
    public class SmtpEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string body)
        {
            return Task.CompletedTask;
        }
    }
}
