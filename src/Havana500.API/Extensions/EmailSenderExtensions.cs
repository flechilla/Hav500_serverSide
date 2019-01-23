using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Havana500.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link, string userFullName, string userRole)
        {
            return emailSender.SendEmailAsync(email, userFullName, "Confirmaci�n de correo",
                $"<p>Por favor confirme su correo a trav�s de <a href='{HtmlEncoder.Default.Encode(link)}'>este</a> link " +
                $"para acceder al sistema de administraci�n de <b>Habana500</b> bajo de el role de: <b>{userRole}</b></p>");
        }
    }
}
