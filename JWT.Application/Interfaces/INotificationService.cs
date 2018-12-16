using System.Threading.Tasks;

namespace JWT.Application.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendNotificationAsync(string toName,
            string toEmailAddress,
            string subject,
            string message);
    }
}
