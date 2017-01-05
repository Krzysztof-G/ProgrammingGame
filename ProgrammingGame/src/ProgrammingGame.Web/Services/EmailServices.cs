using System.Threading.Tasks;

namespace ProgrammingGame.Web.Services
{ 
    public class EmailService : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }
}
