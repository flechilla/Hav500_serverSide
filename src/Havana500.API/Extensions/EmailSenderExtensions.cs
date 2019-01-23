using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Havana500.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link, string userFullName, string userRole)
        {
            return emailSender.SendEmailAsync(email, userFullName, "Confirmación de correo",
                $"<p>Por favor confirme su correo a través de <a href='{HtmlEncoder.Default.Encode(link)}'>este</a> link " +
                $"para acceder al sistema de administración de <b>Habana500</b> bajo de el role de: <b>{userRole}</b></p>");
        }
    }
}
