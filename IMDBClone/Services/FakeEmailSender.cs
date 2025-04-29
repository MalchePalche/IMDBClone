using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace IMDBClone.Services
{
    public class FakeEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"📧 FAKE EMAIL:\nTo: {email}\nSubject: {subject}\nBody:\n{htmlMessage}");
            return Task.CompletedTask;
        }
    }
}
