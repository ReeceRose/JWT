using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace JWT.Infrastructure.Notifications
{
    public interface INotificationService : IEmailSender
    {
        // More features might want to be added in the future so add the ability to extend IEmailSender   
    }
}
