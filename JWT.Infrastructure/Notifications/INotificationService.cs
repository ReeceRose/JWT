using System.Threading.Tasks;

namespace JWT.Infrastructure.Notifications
{
    public interface INotificationService
    {
        Task SendNotificationAsync(
            string toName,
            string toEmailAddress,
            string subject,
            string message
        );
    }
}
