using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace JWT.Infrastructure.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;

        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        public Task Execute(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(_configuration["SendGrid:Key"]);
            var message = new SendGridMessage()
            {
                From = new EmailAddress(_configuration["SendGrid:From"], _configuration["SendGrid:Name"]),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage
            };
            message.AddTo(new EmailAddress(email));
            message.SetClickTracking(false, false);

            return client.SendEmailAsync(message);
        }
    }
}
