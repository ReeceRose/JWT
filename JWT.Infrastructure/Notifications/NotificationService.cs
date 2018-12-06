using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Org.BouncyCastle.Crypto.Tls;

namespace JWT.Infrastructure.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;

        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendNotificationAsync(string fromName, string fromEmailAddress, string toName, string toEmailAddress,
            string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(name: fromName, address: fromEmailAddress));
            email.To.Add(new MailboxAddress(name: toName, address: toEmailAddress));
            email.Subject = subject;
            var body = new BodyBuilder {HtmlBody = message };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (sender, certificate, certChainType, errors) => true;
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                await client.ConnectAsync(_configuration["SMTP:Host"], int.Parse(_configuration["SMTP:Port"]), false)
                    .ConfigureAwait(false);
                await client.AuthenticateAsync(_configuration["SMTP:Username"], _configuration["SMTP:Password"])
                    .ConfigureAwait(false);

                await client.SendAsync(email).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
