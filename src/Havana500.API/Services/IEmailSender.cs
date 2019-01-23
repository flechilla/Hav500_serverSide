using System.Threading.Tasks;

namespace Havana500.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string userFullName, string subject, string message);
    }
}
