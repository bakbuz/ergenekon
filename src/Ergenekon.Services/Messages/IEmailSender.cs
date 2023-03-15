namespace Ergenekon.Services.Messages
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string body);
    }

    public class NullEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string body)
        {
            return Task.CompletedTask;
        }
    }
}
